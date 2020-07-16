using iMed.Commands;
using iMed.intrefaces;
using iMedDataSource;
using iMedShared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


namespace iMed
{
    /// <summary>
    /// 
    /// </summary>
    public class MainVM:
                NotifyModelBase,
                IMultiThreadObservableCollectionConsumer
    {
        private Object _ClientsItemsLock;
        private ObservableCollection<ClientRowModel> _Clients;

        private Object _VisitsItemsLock;
        private ObservableCollection<VisitRowModel> _Visits;

        private ClientRowModel _SelectedClient;
        private VisitRowModel _SelectedVisit;

        private CancellationTokenSource _CancelTokenSource = null;
        private CancellationTokenSource _CancelTokenVisits = null;
        private Task _Task = null;
        private Task _TaskLoadVisits = null;


        private IMessageBoxService _IMessageBoxService = null;

        /// <summary>
        /// 
        /// </summary>
        private String _MessageInfo;

        /// <summary>
        /// 
        /// </summary>
        public String MessageInfo
        {
            get
            {
                return this._MessageInfo;
            }

            set
            {
                if( 0 != String.Compare( value, this._MessageInfo, true ) )
                {
                    this._MessageInfo = value;
                    OnPropertyChanged( "MessageInfo" );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ClientRowModel> Clients
        {
            get
            {
                return this._Clients;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<VisitRowModel> Visits
        {
            get
            {
                return this._Visits;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientRowModel SelectedClient
        {
            get
            {
                return this._SelectedClient;
            }

            set
            {
                if( this._SelectedClient != value )
                {
                    this._SelectedClient = value;
                    if( null  == value )
                    {
                        this.SelectedVisit = null;
                        this.Visits.Clear();
                    }
                    OnPropertyChanged( "SelectedClient" );
                    RefreshVisits( false );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public VisitRowModel SelectedVisit
        {
            get
            {
                return this._SelectedVisit;
            }

            set
            {
                if( this._SelectedVisit != value )
                {
                    this._SelectedVisit = value;
                    OnPropertyChanged( "SelectedVisit" );
                }
            }
        }

        private String _SearchClientsContent;

        /// <summary>
        /// 
        /// </summary>
        public String SearchClientsContent
        {
            get
            {

                return this._SearchClientsContent;
            }
            set
            {
                if( 0 != String.Compare( value, this._SearchClientsContent, true ) )
                {
                    this._SearchClientsContent = value;
                    RefreshClients( true );
                }

            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public MainVM( IMessageBoxService iMessageBoxService )
        {

            this._IMessageBoxService = iMessageBoxService;

            this._SelectedClient = null;
            this._SelectedVisit = null;

            this._ClientsItemsLock = new object();
            this._Clients = new ObservableCollection<ClientRowModel>();
            BindingOperations.EnableCollectionSynchronization( this._Clients,
                                                               this._ClientsItemsLock );

            this._VisitsItemsLock = new object();
            this._Visits = new ObservableCollection<VisitRowModel>();
            BindingOperations.EnableCollectionSynchronization( this._Visits,
                                                               this._VisitsItemsLock );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCollectionConsumer"></param>
        /// <param name="token"></param>
        /// <param name="delay"></param>
        private void LoadClients( IMultiThreadObservableCollectionConsumer iCollectionConsumer,
                                   CancellationToken token, Boolean delay )
        {
            try
            {
                if( delay )
                {
                    Debug.WriteLine( "Load clients: sleep 500 ms" );
                    Thread.Sleep( 500 );
                    if( token.IsCancellationRequested )
                    {
                        Debug.WriteLine( "Load clients cancelled: task break without load" );
                        return;
                    }
                }

                IDataSource data_source = DataSourceCreator.CreateDataSource();
                var clients = data_source.LoadClients( this.SearchClientsContent );
                iCollectionConsumer.ClearClients();

                foreach( var client in clients )
                {
                    if( token.IsCancellationRequested )
                    {
                        Debug.WriteLine( "Load clients cancelled: break add itens" );
                        break;
                    }

                    iCollectionConsumer.AddClient( client );
                }
            }catch(Exception ex )
            {
                this._IMessageBoxService.MessageBoxShow( "Error", ex.Message,
                                    MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCollectionConsumer"></param>
        /// <param name="token"></param>
        /// <param name="delay"></param>
        /// <param name="clientId"></param>
        private void LoadVisits( IMultiThreadObservableCollectionConsumer iCollectionConsumer,
                                CancellationToken token, Boolean delay,
                                Int32 clientId )
        {
            try
            {
                if( delay )
                {
                    Debug.WriteLine( "Load visits: sleep 500 ms" );
                    Thread.Sleep( 500 );
                    if( token.IsCancellationRequested )
                    {
                        Debug.WriteLine( "Load visits cancelled: task break without load" );
                        return;
                    }
                }

                IDataSource data_source = DataSourceCreator.CreateDataSource();
                var visits = data_source.LoadVisits( clientId );
                iCollectionConsumer.ClearVisits();

                foreach( var visit in visits )
                {
                    if( token.IsCancellationRequested )
                    {
                        Debug.WriteLine( "Load visits cancelled: break add itens" );
                        break;
                    }

                    iCollectionConsumer.AddVisit( visit );
                }
            }
            catch( Exception ex )
            {
                this._IMessageBoxService.MessageBoxShow( "Error", ex.Message,
                                    MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay"></param>
        public void RefreshClients( Boolean delay )
        {

            if( null != this._CancelTokenSource )
            {
                _CancelTokenSource.Cancel();
            }

            this._CancelTokenSource = new CancellationTokenSource();
            CancellationToken token = _CancelTokenSource.Token;

            if( null == this._Task )
            {
                this._Task = new Task( () =>
                {
                    LoadClients( this, token, delay );                    
                } );

                this._Task.Start();
            }
            else
            {
                this._Task = this._Task.ContinueWith( obj =>
                {
                    LoadClients( this, token, delay );
                } );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay"></param>
        public void RefreshVisits( Boolean delay )
        {
            if( this.SelectedClient == null )
            {
                return;
            }

            var client_id = this.SelectedClient.Id;

            if( null != this._CancelTokenVisits )
            {
                _CancelTokenVisits.Cancel();
            }

            this._CancelTokenVisits = new CancellationTokenSource();
            CancellationToken token = _CancelTokenVisits.Token;

            if( null == this._TaskLoadVisits )
            {
                this._TaskLoadVisits = new Task( ( ) => LoadVisits( this, token, delay,
                                                                    client_id ) );
                this._TaskLoadVisits.Start();
            }
            else
            {
                this._TaskLoadVisits = this._TaskLoadVisits.ContinueWith( obj =>
                {
                    LoadVisits( this, token, delay, client_id );
                } );
            }
        }

        #region IMultiThreadObservableCollectionConsumer
        /// <summary>
        /// 
        /// </summary>
        public void ClearClients( )
        {
            lock( this._ClientsItemsLock )
            {
                this.SelectedClient = null;
                this.Clients.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearVisits( )
        {
            lock( this._VisitsItemsLock )
            {
                this.SelectedVisit = null;
                this.Visits.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        public void AddClient( ClientRowModel newItem )
        {
            lock( this._ClientsItemsLock )
            {
                this._Clients.Add( newItem );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        public void AddVisit( VisitRowModel newItem )
        {
            lock( this._VisitsItemsLock )
            {
                this._Visits.Add( newItem );
            }
        }
        #endregion

        #region COMMANDS

        private RelayCommand _RefreshCmd;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand RefreshCmd
        {
            get
            {
                return this._RefreshCmd ??
                    ( this._RefreshCmd = new RelayCommand( 
                        ( obj ) => 
                        {
                            RefreshClients( false );
                        },
                        ( obj ) => 
                        { 
                            return true; 
                        } )
                    );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private OpenDialogCommand _ClientEditCmd;

        /// <summary>
        /// 
        /// </summary>
        public OpenDialogCommand ClientEditCmd
        {
            get
            {
                return this._ClientEditCmd ??
                    ( this._ClientEditCmd = new OpenDialogCommand( IsClientSelected,
                                                                   OnLoadClientInfo ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private OpenDialogCommand _ClientCreateCmd;

        /// <summary>
        /// 
        /// </summary>
        public OpenDialogCommand ClientCreateCmd
        {
            get
            {
                return this._ClientCreateCmd ??
                    ( this._ClientCreateCmd = new OpenDialogCommand( null,
                                                                     OnCreateClientInfo ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private RelayCommand _ClientDeleteCmd;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ClientDeleteCmd
        {
            get
            {
                return this._ClientDeleteCmd ??
                    ( this._ClientDeleteCmd = new RelayCommand( ( obj ) => 
                    {
                        try
                        {
                            MessageBoxResult result = MessageBoxResult.Yes;

                            if( null != this._IMessageBoxService )
                            {
                                result = this._IMessageBoxService.MessageBoxShow( "Warning", "Delete client?",
                                                                                  MessageBoxButton.YesNo,
                                                                                  MessageBoxImage.Question );
                                if( result == MessageBoxResult.Yes )
                                {
                                    IDataSource i_datasource = DataSourceCreator.CreateDataSource();
                                    i_datasource.DeleteClientById( this.SelectedClient.Id );
                                }
                            }

                        }catch( Exception ex )
                        {
                            Debug.WriteLine( ex.Message );
                        }
                    },
                    ( obj ) => { return IsClientSelected(); } )
                    );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private OpenDialogCommand _VisitEditCmd;

        /// <summary>
        /// 
        /// </summary>
        public OpenDialogCommand VisitEditCmd
        {
            get
            {
                return this._VisitEditCmd ??
                    ( this._VisitEditCmd = new OpenDialogCommand( IsVisitSelected,
                                                                  OnLoadVisitInfo ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private OpenDialogCommand _VisitCreateCmd;

        /// <summary>
        /// 
        /// </summary>
        public OpenDialogCommand VisitCreateCmd
        {
            get
            {
                return this._VisitCreateCmd ??
                    ( this._VisitCreateCmd = new OpenDialogCommand( IsClientSelected,
                                                                   OnCreateVisitInfo ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private RelayCommand _VisitDeleteCmd;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand VisitDeleteCmd
        {
            get
            {
                return this._VisitDeleteCmd ??
                    ( this._VisitDeleteCmd = new RelayCommand( 
                                  ( obj ) =>
                                  {
                                      try
                                      {
                                          MessageBoxResult result = MessageBoxResult.Yes;

                                          if( null != this._IMessageBoxService )
                                          {
                                              result = this._IMessageBoxService.MessageBoxShow( "Warning", "Delete visit?",
                                                                                                MessageBoxButton.YesNo,
                                                                                                MessageBoxImage.Question );
                                              if( result == MessageBoxResult.Yes )
                                              {
                                                  IDataSource i_datasource = DataSourceCreator.CreateDataSource();
                                                  i_datasource.DeleteVisitById( this.SelectedVisit.Id );
                                              }
                                          }

                                      }
                                      catch( Exception ex )
                                      {
                                          Debug.WriteLine( ex.Message );
                                      }
                                  },
                                 ( obj ) => 
                                 { 
                                     return IsVisitSelected(); 
                                 } ) );
            }
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean IsClientSelected( Object parameter = null )
        {
            return ( this._SelectedClient != null );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private Boolean IsVisitSelected( Object parameter = null )
        {
            return ( this._SelectedVisit != null );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private ClientInfoModel OnLoadClientInfo( Object parameter )
        {
            ClientInfoModel model = null;

            if( null != this.SelectedClient )
            {
                model = LoadClientModel( this.SelectedClient.Id );
            }

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private VisitInfoModel OnLoadVisitInfo( Object parameter )
        {
            VisitInfoModel model = null;

            if( null != this.SelectedVisit )
            {
                model = LoadVisitModel( this.SelectedVisit.Id );
            }

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private VisitInfoModel OnCreateVisitInfo( Object parameter )
        {
            VisitInfoModel model = LoadVisitModel( 0 );

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private ClientInfoModel OnCreateClientInfo( Object parameter )
        {
            ClientInfoModel model = LoadClientModel( 0 );

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ClientInfoModel LoadClientModel( Int32 id )
        { 
            IDataSource i_datasource = DataSourceCreator.CreateDataSource();
            
            ClientInfoModel model = i_datasource.LoadClientInfo( id );
            
            model.SuccessSave += ( o, e ) =>
            {
                if( 0 == id )
                {   // add new
                    var new_client = new ClientRowModel( model.Id,
                                                      model.Name,
                                                      model.SecondName,
                                                      model.ThirdName );
                    this.AddClient( new_client );
                    this.SelectedClient = new_client;
                }
                else
                { // refresh
                    var client = this.Clients.Where( c => c.Id == model.Id ).
                                          FirstOrDefault();
                    if( null != client )
                    {
                        client.Name = model.Name;
                        client.SecondName = model.SecondName;
                        client.ThirdName = model.ThirdName;
                    }
                    this.SelectedClient = client;
                }
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private VisitInfoModel LoadVisitModel( Int32 id )
        {
            
            IDataSource i_datasource = DataSourceCreator.CreateDataSource();

            VisitInfoModel model = i_datasource.LoadVisitInfo( id );

            if( id == 0 )
            {
                model.ClientId = this.SelectedClient.Id;
            }

            model.SuccessSave += ( o, e ) =>
            {
                // испольцуем model - так ьыстрее

                if( 0 == id )
                {   // add new
                    var new_visit = new VisitRowModel( model.Id,
                                                        model.ClientId,
                                                        model.DateVisit,
                                                        model.VisitType,
                                                        model.Diagnos );
                    this.AddVisit( new_visit );
                }
                else
                { // refresh
                    var visit = this.Visits.Where( c => c.Id == model.Id ).
                                             FirstOrDefault();
                    if( null != visit )
                    {
                        visit.DateVisit = model.DateVisit;
                        visit.Diagnos = model.Diagnos;
                        visit.VisitType =model.VisitType;
                    }                
                }
            };

            return model;
        }
    }
}
