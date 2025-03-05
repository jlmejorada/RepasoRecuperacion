using ENT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
using System.Collections.ObjectModel;

namespace DAL
{
    public class ManejadoraAPI
    {
        static String uri = "https://localhost:7103/API/Personas";
        public static async Task<ObservableCollection<Personas>> getPersonas()
        {
            ObservableCollection<Personas> listaPersonas = new ObservableCollection<Personas>();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            string textoJsonRes;

            try
            {
                response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    textoJsonRes = await httpClient.GetStringAsync(uri);
                    httpClient.Dispose();
                    listaPersonas = JsonConvert.DeserializeObject<ObservableCollection<Personas>>(textoJsonRes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaPersonas;
        }


        public async Task<HttpStatusCode> insertarPersona(Personas per)
        {
            HttpClient mihttpClient = new HttpClient();
            string datos;
            HttpContent contenido;
            Uri miUri = new Uri($"{uri}Personas");
            HttpResponseMessage miRespuesta = new HttpResponseMessage();
            try
            {
                datos = JsonConvert.SerializeObject(per);
                contenido = new StringContent(datos, System.Text.Encoding.UTF8, "application/json");
                miRespuesta = await mihttpClient.PostAsync(miUri, contenido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return miRespuesta.StatusCode;
        }
    }  
}
