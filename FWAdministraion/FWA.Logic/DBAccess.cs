using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FWA.Logic.Mappings;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FWA.Logic
{
    public static class DBAccess
    {
        public static ISessionFactory SessionFactory { get; }

        static DBAccess()
        {
            SessionFactory = BuildSessionFactory();
        }

        private static ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure()
                    .Database(GetDatabase())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMapping>())
                    .ExposeConfiguration(c => new SchemaUpdate(c).Execute(true, true))
                    .BuildSessionFactory();
        }

        private static IPersistenceConfigurer GetDatabase()
        {
            return MySQLConfiguration.Standard.ConnectionString(x => x.Server("127.0.0.1")
                                                                      .Database("fwadministration")
                                                                      .Username("fwa_app")
                                                                      .Password("r@Fckx!3bbK7nRbh"));
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void Insert(object obj, InsertionMode mode = InsertionMode.SaveOrUpdate, ISession session = null)
        {
            ExecuteInTransaction(x =>
            {
                switch (mode)
                {
                    case InsertionMode.Save:
                        x.Save(obj); break;
                    case InsertionMode.Update:
                        x.Update(obj); break;
                    default:
                        x.SaveOrUpdate(obj); break;
                }
            }, session);
        }

        public static T GetById<T>(object id, ISession session = null)
        {
            return ExecuteFuncInTransaction(s => s.Get<T>(id), session);
        }

        public static List<T> GetByCriteria<T>(Action<ICriteria> criteriaEditor, ISession session = null)
            where T : class
        {
            return ExecuteFuncInTransaction(s =>
            {
                var criteria = s.CreateCriteria<T>();
                criteriaEditor.Invoke(criteria);
                return criteria.List<T>().ToList();
            }, session);
        }

        public static void ExecuteInTransaction(Action<ISession> action, ISession session = null)
        {
            ExecuteFuncInTransaction(x =>
            {
                action.Invoke(x); return true;
            }, session);
        }

        public static T ExecuteFuncInTransaction<T>(Func<ISession, T> func, ISession session = null)
        {
            return ExecuteInNewOrExistingSession(s => 
            {
                ITransaction transaction = null;
                try
                {
                    transaction = s.BeginTransaction();
                    var result = func.Invoke(s);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    if (transaction != null)
                        transaction.Rollback();
                    throw;
                }
            }, session);
        }

        private static T ExecuteInNewOrExistingSession<T>(Func<ISession, T> func, ISession session)
        {
            bool newSession = false;
            T result;
            try
            {
                if (session == null)
                {
                    session = SessionFactory.OpenSession();
                    newSession = true;
                }
                result = func.Invoke(session);
            }
            finally
            {
                if (newSession && session != null)
                    session.Close();
            }
            return result;
        }
    }

    public enum InsertionMode
    {
        Save, Update, SaveOrUpdate
    }
}
