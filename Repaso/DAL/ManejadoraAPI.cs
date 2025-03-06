using DTO;
using ENT;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ManejadoraAPI
    {
        static string uri = "https://localhost:7103/API/Personas";

        public static async Task<ObservableCollection<Personas>> ObtenerListado()
        {
            ObservableCollection<Personas> listado = new ObservableCollection<Personas>();

            using HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string textoJsonRes = await response.Content.ReadAsStringAsync();
                    var personas = JsonConvert.DeserializeObject<ObservableCollection<Personas>>(textoJsonRes);

                    if (personas != null)
                    {
                        listado = personas;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos", ex);
            }

            return listado;
        }

        public static async Task<Personas> ObtenerObjMAUI(int id)
        {
            string miUri = $"{uri}/{id}";
            Personas obj = null;

            using HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(miUri);
                if (response.IsSuccessStatusCode)
                {
                    string textoJsonRespuesta = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<Personas>(textoJsonRespuesta) ?? new Personas();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el objeto", ex);
            }

            return obj ?? new Personas();
        }

        public static async Task<bool> InsertarObjMAUI(Personas obj)
        {
            using HttpClient httpClient = new HttpClient();

            try
            {
                string json = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await httpClient.PostAsync(uri, content);
                return respuesta.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el objeto", ex);
            }
        }

        public static async Task<bool> ActualizarObj(Personas obj)
        {
            string miUri = $"{uri}/{obj.Id}";
            using HttpClient httpClient = new HttpClient();

            try
            {
                string json = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await httpClient.PutAsync(miUri, content);
                return respuesta.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el objeto", ex);
            }
        }

        public static async Task<bool> EliminarObj(int id)
        {
            string miUri = $"{uri}/{id}";
            using HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage respuesta = await httpClient.DeleteAsync(miUri);
                return respuesta.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el objeto", ex);
            }
        }
    }
}
