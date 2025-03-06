using APIMaui.VM.Util;
using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMaui.VM
{
    [QueryProperty(nameof(Persona), "persona")]
    public class ActualizaVM : clsVMBase
    {
        #region ATRIBUTOS
        private Personas persona;
        private DelegateCommand btnVolverCmd;
        private DelegateCommand btnGuardarCmd;
        #endregion

        #region PROPIEDADES
        public Personas Persona
        {
            get { return persona; }
            set
            {
                persona = value;
                OnPropertyChanged("Persona");
            }
        }
        public DelegateCommand BtnVolverCmd { get { return btnVolverCmd; } }
        public DelegateCommand BtnGuardarCmd { get { return btnGuardarCmd; } }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Constructor por defectos
        /// </summary>
        public ActualizaVM()
        {
            btnVolverCmd = new DelegateCommand(btnVolverCmdExecute);
            btnGuardarCmd = new DelegateCommand(btnGuardarCmdExecute);
        }
        #endregion

        #region FUNCIONES
        private async void btnVolverCmdExecute()
        {
            await Shell.Current.GoToAsync("///listaPersona");
        }
        private async void btnGuardarCmdExecute()
        {
            await ManejadoraAPI.ActualizarObj(persona);
            await Shell.Current.GoToAsync("///listaPersona");
        }
        #endregion
    }
}
