using FEP.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Data
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory(); return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()

             .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                @"Data Source=.;Initial Catalog=SneakerManagement;Integrated Security=True").ShowSql())

             .Mappings(m => m.FluentMappings
                 .AddFromAssemblyOf<Sneaker>()
             )
             .ExposeConfiguration(cfg => new SchemaExport(cfg)
             .Create(false, false))
             .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}