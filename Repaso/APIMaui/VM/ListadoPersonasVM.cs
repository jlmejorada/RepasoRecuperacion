using APIMaui.VM.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT;
using DAL;

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
            // Eliminamos la llamada a cargaLista aquí
            btnDetallesCmd = new DelegateCommand(btnDetallesCmdExecute, btnCmdCanExecute);
            btnEditarCmd = new DelegateCommand(btnEditaCmdExecute, btnCmdCanExecute);
            btnBorrarCmd = new DelegateCommand(btnBorraCmdExecute, btnCmdCanExecute);
            btnAnadirCmd = new DelegateCommand(btnAnadeCmdExecute);
        }
        #endregion

        #region FUNCIONES
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
                 {"idPersona",  persona.Id}
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

        private async void btnBorraCmdExecute()
        {
            await ManejadoraAPI.EliminarObj(personaSeleccionada.Id);
            lista.Remove(personaSeleccionada);
            OnPropertyChanged("Lista");
        }

        // Esta función se llama cuando la vista aparece
        public async void cargaLista()
        {
            lista.Clear();
            ObservableCollection<Personas> prueba = await DAL.ManejadoraAPI.ObtenerListado();
            foreach (Personas persona in prueba)
            {
                lista.Add(persona);
            }
            OnPropertyChanged(nameof(lista));
        }
        #endregion
    }
}
