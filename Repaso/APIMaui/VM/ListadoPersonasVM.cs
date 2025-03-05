using APIMaui.VM.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT;

namespace APIMaui.VM
{
    public class ListadoPersonasVM : clsVMBase
    {



        #region ATRIBUTO
        private ObservableCollection<Personas> lista = new ObservableCollection<Personas>();
        private Personas personaSeleccionada;
        private DelegateCommand btnDetallesCmd;
        private DelegateCommand btnEditarCmd;
        private DelegateCommand btnBorrarCmd;
        private DelegateCommand btnAnadirCmd;
        #endregion

        #region PROPIEDAD
        public ObservableCollection<Personas> Lista { get { return lista; } set { lista = value; } }
        public Personas PersonaSeleccionada
        {
            get { return personaSeleccionada; }
            set
            {
                personaSeleccionada = value;
                OnPropertyChanged("PersonaSeleccionada");

                btnDetallesCmd.RaiseCanExecuteChanged();
                btnEditarCmd.RaiseCanExecuteChanged();
                btnBorrarCmd.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand BtnDetallesCmd { get { return btnDetallesCmd; } }
        public DelegateCommand BtnEditarCmd { get { return btnEditarCmd; } }
        public DelegateCommand BtnBorrarCmd { get { return btnBorrarCmd; } }
        public DelegateCommand BtnAnadirCmd { get { return btnAnadirCmd; } }
        #endregion

        #region CONSTRUCTOR
        public ListadoPersonasVM()
        {
            cargaLista();

            btnDetallesCmd = new DelegateCommand(btnDetallesCmdExecute, btnCmdCanExecute);
            btnEditarCmd = new DelegateCommand(btnEditaCmdExecute, btnCmdCanExecute);
            btnBorrarCmd = new DelegateCommand(btnBorraCmdExecute, btnCmdCanExecute);
            btnAnadirCmd = new DelegateCommand(btnAnadeCmdExecute);
        }
        #endregion


        #region FUNCIONES
        /// <summary>
        /// Funcion que comprueba si se ha seleccionado a una persona
        /// </summary>
        /// <returns></returns>
        private bool btnCmdCanExecute()
        {
            bool estaSeleccionada = false;
            if (personaSeleccionada != null)
            {
                estaSeleccionada = true;
            }
            return estaSeleccionada;
        }
        private async void btnDetallesCmdExecute()
        {
            Personas persona = personaSeleccionada;
            var queryParams = new Dictionary<string, Object>
            {
                 {"persona",  persona}
            };

            await Shell.Current.GoToAsync("///detallesPersona", queryParams);
        }
        private async void btnEditaCmdExecute()
        {
            Personas persona = new Personas(PersonaSeleccionada);
            var queryParams = new Dictionary<string, Object>
            {
                 {"persona",  persona}
            };

            await Shell.Current.GoToAsync("///editaPersona", queryParams);
        }

        private async void btnAnadeCmdExecute()
        {
            await Shell.Current.GoToAsync("///anadePersona");
        }

        private void btnBorraCmdExecute()
        {
            Personas persona = personaSeleccionada;
            try
            {
                //ClsManejadoraPersonaBL.BorraPersonaBL(persona.Id);
                //lista.Remove(persona);
                //NotifyPropertyChanged("Lista");
            }
            catch
            {

            }
        }

        private async void cargaLista()
        {
            ObservableCollection<Personas> prueba = await DAL.ManejadoraAPI.getPersonas();
            foreach (Personas persona in prueba)
            {
                lista.Add(persona);
            }
        }
        #endregion

    }
}
