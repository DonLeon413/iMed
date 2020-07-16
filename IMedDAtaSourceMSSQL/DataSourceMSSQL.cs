using iMedDataSource;
using IMedDataSourceMSSQL.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace IMedDataSourceMSSQL
{
    /// <summary>
    /// MS SQL DATA SOURCE
    /// </summary>
    public class DataSourceMSSQL:
                IDataSource
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public DataSourceMSSQL( )
        {
        }

        #region IDataSource

        /// <summary>
        /// 
        /// </summary>
        /// <param name="findContext"></param>
        /// <returns></returns>
        public IEnumerable<ClientRowModel> LoadClients( String findContext = null )
        {
            //  separate to tokens
            var tokens = BuildFindContext( findContext );

            using( var db_context = new iMedDataContext() )
            {
                IQueryable<Client> query = db_context.Clients;

                // Add filter
                if( tokens.Length > 0 )
                {
                    query = query.Where( c => tokens.Any( x => c.Name.Contains( x ) ) ||
                                              tokens.Any( x => c.SecondName.Contains( x ) ) ||
                                              tokens.Any( x => c.ThirdName.Contains( x ) )
                                        );
                }

                return query.ToList().Select( c => new ClientRowModel( c.Id, c.Name, c.SecondName,
                                                                       c.ThirdName ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientInfoModel LoadClientInfo( Int32 id )
        {
            ClientInfoModel result = null;

            if( id == 0 )
            {   // может в будущем еще надо сделать
                result = new ClientInfoModel( SaveClientInfo );
            }
            else
            {
                using( var db_context = new iMedDataContext() )
                {
                    var client_bbdd = db_context.Clients.Where( c => c.Id == id ).
                                                         FirstOrDefault();
                    if( client_bbdd == null )
                    {
                        throw new IMedDataSourceException( String.Format( "Client with id:{0} not found", id ) );
                    }

                    result = new ClientInfoModel( SaveClientInfo )
                    {
                        Id = client_bbdd.Id,
                        Name = client_bbdd.Name,
                        SecondName = client_bbdd.SecondName,
                        ThirdName = client_bbdd.ThirdName,
                        BirthDay = client_bbdd.DateBirth,
                        Address = client_bbdd.Address
                    };
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void SaveClientInfo( ClientInfoModel model )
        {
            using( var db_context = new iMedDataContext() )
            {
                Client client_bbdd = null;
                if( 0 == model.Id )
                {
                    client_bbdd = new Client();
                }
                else
                {
                    client_bbdd = db_context.Clients.Where( c => c.Id == model.Id ).
                                                     FirstOrDefault();
                    if( null == client_bbdd )
                    {
                        throw new IMedDataSourceException( String.Format( "Client with id:{0} not found",
                                                                          model.Id ) );
                    }
                }

                client_bbdd.Name = model.Name;
                client_bbdd.SecondName = model.SecondName;
                client_bbdd.ThirdName = model.ThirdName;
                client_bbdd.DateBirth = model.BirthDay;
                client_bbdd.Address = model.Address;

                if( 0 == model.Id )
                {
                    db_context.Clients.Add( client_bbdd );
                }

                db_context.SaveChanges();

                model.Id = client_bbdd.Id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClientById( Int32 id )
        {
            using( var db_context = new iMedDataContext() )
            {

                Client client = new Client()
                {
                    Id = id
                };

                db_context.Entry( client ).State = EntityState.Deleted;

                db_context.SaveChanges();
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="clientId"></param>
       /// <returns></returns>
        public IEnumerable<VisitRowModel> LoadVisits( Int32 clientId )
        {
            using( var db_context = new iMedDataContext() )
            {
                var query = db_context.Visits.Where( v => v.ClientId == clientId );

                return query.ToList().Select( v => new VisitRowModel( v.Id, v.ClientId,
                                                                   v.VisitData, 
                                                                   v.VisitType, v.Diagnos ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VisitInfoModel LoadVisitInfo( Int32 id )
        {
            VisitInfoModel result = null;
            if( 0 == id )
            {
                result = new VisitInfoModel( SaveVisitInfo );                
            }
            else
            {
                using( var db_context = new iMedDataContext() )
                {
                    var visit_bbdd = db_context.Visits.Where( v => v.Id == id ).
                                                       FirstOrDefault();
                    
                    if( visit_bbdd == null )
                    {
                        throw new IMedDataSourceException( String.Format( "Visit with id:{0} not found", id ) );
                    }

                    result = new VisitInfoModel( SaveVisitInfo )
                    {
                        Id = visit_bbdd.Id,
                        ClientId = visit_bbdd.ClientId,
                        Diagnos = visit_bbdd.Diagnos,
                        DateVisit = visit_bbdd.VisitData,
                        VisitType = visit_bbdd.VisitType
                    };
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>        
        public void SaveVisitInfo( VisitInfoModel model )
        {
            using( var db_context = new iMedDataContext() )
            {
                if( 0 == model.Id )
                {
                    var visit_bbdd = new Visit()
                    {
                        ClientId = model.ClientId,
                        Diagnos = model.Diagnos,
                        VisitData = model.DateVisit,
                        VisitType = model.VisitType
                    };

                    db_context.Visits.Add( visit_bbdd );
                }
                else
                {
                    var visit_bbdd = db_context.Visits.Where( v => v.Id == model.Id ).
                                                       FirstOrDefault();
                    if( visit_bbdd == null )
                    {
                        throw new IMedDataSourceException( String.Format( "Visit with id:{0} not found", model.Id ) );
                    }

                    visit_bbdd.ClientId = model.ClientId;
                    visit_bbdd.Diagnos = model.Diagnos;
                    visit_bbdd.VisitData = model.DateVisit;
                    visit_bbdd.VisitType = model.VisitType;
                }

                db_context.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteVisitById( Int32 id )
        {
            using( var db_context = new iMedDataContext() )
            {

                Visit visit = new Visit()
                {
                    Id = id
                };

                db_context.Entry( visit ).State = EntityState.Deleted;

                db_context.SaveChanges();
            }
        }
        #endregion

        #region UTILS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="findContext"></param>
        /// <returns></returns>
        private String[] BuildFindContext( String findContext )
        {
            if( String.IsNullOrWhiteSpace( findContext ) )
            {
                return new String[0];
            }

            return findContext.Split( new char[] { ' ' } ).
                                Where( t => false == String.IsNullOrWhiteSpace( t ) ).
                                Select( t => t.Trim() ).
                                ToArray();
        }
               
        #endregion
    } 
}