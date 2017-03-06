using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FWA.Core.Models;
using FWA.Core.Models.Mappings;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FWA.Core.Helpers
{
   internal static class DBAccess
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
         return MsSqlConfiguration.MsSql2012.ConnectionString(x => x.Server("Markus-PC")
                                                                   .Database("FWA")
                                                                   .Username("sa")
                                                                   .Password("Vivendi2016"));
      }

      public static ISession OpenSession()
      {
         return SessionFactory.OpenSession();
      }

      public static void InsertMultiple(IEnumerable objects, InsertionMode mode = InsertionMode.SaveOrUpdate, ISession session = null)
      {
         ExecuteInTransaction(x =>
         {
            foreach (var obj in objects)
            {
               InternalInsert(obj, mode, x);
            }
         }, session);
      }

      public static void Insert(object obj, InsertionMode mode = InsertionMode.SaveOrUpdate, ISession session = null)
      {
         ExecuteInTransaction(x => InternalInsert(obj, mode, x), session);
      }

      private static void InternalInsert(object obj, InsertionMode mode, ISession session)
      {
         switch (mode)
         {
            case InsertionMode.Save:
               session.Save(obj); break;
            case InsertionMode.Update:
               session.Update(obj); break;
            default:
               session.SaveOrUpdate(obj); break;
         }
      }

      public static T GetById<T>(object id, ISession session = null)
      {
         return ExecuteFuncInTransaction(s => s.Get<T>(id), session);
      }

      public static List<Gegenstand> GetItemsLikeInvNummer(string invNummerLike, string bezeichnung)
      {
         var session = OpenSession();
         var items = session.Query<Gegenstand>().Where(g => g.Bezeichnung == bezeichnung && g.InvNummer.Like(invNummerLike)).ToList();
         session.Close();

         return items;
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

   /// <summary>
   /// Gibt an, ob das Objekt in die Datenbank neu eingefügt oder aktualisiert werden soll
   /// </summary>
   public enum InsertionMode
   {
      /// <summary>
      /// Das angegebene Objekt wird als neue Instanz gespeichert
      /// </summary>
      Save,

      /// <summary>
      /// Eine bestehende Instanz des Objekts wird aktualisiert bzw überschrieben
      /// </summary>
      Update,

      /// <summary>
      /// Das System wählt die passende Methode automatisch aus
      /// </summary>
      SaveOrUpdate
   }
}
