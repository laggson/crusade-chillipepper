using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FWA.Logic.Mappings;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;

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
                                                                      .Password("kacken123"));
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void Insert(object obj, InsertionMode mode = InsertionMode.SaveOrUpdate, ISession session = null)
        {
            Execute(x =>
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

        public static T GetById<T>(object id)
        {
            return ExecuteFunc(x => x.Get<T>(id));
        }

        public static void Execute(Action<ISession> action, ISession session = null)
        {
            ExecuteFunc(x =>
            {
                action.Invoke(x); return true;
            }, session);
        }

        public static T ExecuteFunc<T>(Func<ISession, T> func, ISession session = null)
        {
            ITransaction transaction = null;
            bool newSession = false;
            try
            {
                if (session == null)
                {
                    session = SessionFactory.OpenSession();
                    newSession = true;
                }

                transaction = session.BeginTransaction();
                var result = func.Invoke(session);
                transaction.Commit();
                return result;
            }
            catch
            {
                if (transaction != null)
                    transaction.Rollback();
                throw;
            }
            finally
            {
                if (newSession && session != null)
                    session.Close();
            }
        }
    }
}
