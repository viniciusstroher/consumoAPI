using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumoAnimalsAPI
{
    public class Program
    {
        public static System.Net.Http.HttpClient httpClient;
        public static string token = "882b1bf1-fa10-4b37-8f67-fbd9e1fee68f";

        static void Main(string[] args)
        {
            httpClient = new System.Net.Http.HttpClient { BaseAddress = new System.Uri("http://localhost:56292/") };

            AtualizarRegistro();
            InserirRegistro();
            Requisitar();
            Console.ReadLine();
        }

        public static void Requisitar()
        {
            try
            {
                httpClient.DefaultRequestHeaders.Add("AuthToken", token);

                HttpResponseMessage post = httpClient.PostAsync("GetAllStates", null).Result;

                if (!post.IsSuccessStatusCode)
                {
                    Console.WriteLine("Problemas no request");
                    return;
                }

                //CaruanaConsumoControlePagamentosViewModel.Models.Pagamento retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<CaruanaConsumoControlePagamentosViewModel.Models.Pagamento>(this.FormatarObjetoRecebido(get.Content.ReadAsStringAsync().Result));
                string retorno = post.Content.ReadAsStringAsync().Result;
                List<ViewModels.EstadoAnimalViewModel> estados = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ViewModels.EstadoAnimalViewModel>>(FormatarObjetoRecebido(retorno));

                if (estados.Any()) { 
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public static void InserirRegistro() {
            try
            {
                ViewModels.RegistroAnimalViewModel viewModel = new ViewModels.RegistroAnimalViewModel();
                viewModel.Descricao = "teste 1 de inserção";
                viewModel.IdEstadoAnimal = 2;
                viewModel.IdSituacaoAnimal = 4;
                viewModel.IdTipoAnimal = 2;
                viewModel.Latitude = 2.4560f;
                viewModel.Longitude = 7.1234567f;

                httpClient.DefaultRequestHeaders.Add("AuthToken", token);
                HttpResponseMessage post = httpClient.PostAsJsonAsync("SavePetLocation", viewModel).Result;

                if (!post.IsSuccessStatusCode)
                {
                    Console.WriteLine("Problemas no request");
                    return;
                }

                string retorno = post.Content.ReadAsStringAsync().Result;

                if (retorno == "true") { }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public static void AtualizarRegistro()
        {
            try
            {
                ViewModels.RegistroAnimalViewModel viewModel = new ViewModels.RegistroAnimalViewModel();
                viewModel.IdRegistroAnimal = 1;
                viewModel.Descricao = "teste de atualização";
                viewModel.IdEstadoAnimal = 4;
                viewModel.IdSituacaoAnimal = 6;
                viewModel.Latitude = 5.999f;
                viewModel.Longitude = 15.12312f;

                httpClient.DefaultRequestHeaders.Add("AuthToken", token);
                HttpResponseMessage post = httpClient.PostAsJsonAsync("UpdatePetLocation", viewModel).Result;

                if (!post.IsSuccessStatusCode)
                {
                    Console.WriteLine("Problemas no request");
                    return;
                }

                string retorno = post.Content.ReadAsStringAsync().Result;

                if (retorno == "true") { }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        private static string FormatarStringRecebida(string dado)
        {
            return dado.Replace("\"", "").Replace("\\", "");
        }

        private static string FormatarObjetoRecebido(string dado)
        {
            string dadoFormatado = dado.Replace("\\", "");

            if (dadoFormatado.StartsWith("\""))
                dadoFormatado = dadoFormatado.Remove(0, 1);

            if (dadoFormatado.EndsWith("\""))
                dadoFormatado = dadoFormatado.Remove(dadoFormatado.Length - 1, 1);

            return dadoFormatado;
        }
    }
}
