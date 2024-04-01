using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class DepartamentosApiClient : BaseClient
    {
        private const string tokenAutenticacao = "AEEFC184-9F62-4B3E-BB93-BE42BF0FFA36";

        private const string departamentosListUrl = "api/Departamentos/Listar";
        private const string departamentosAdicionarUrl = "api/Departamentos/Adicionar";
        private const string departamentosAtualizarUrl = "api/Departamentos/Atualizar";
        private const string departamentosExcluirUrl = "api/Departamentos/Excluir";
        private const string departamentosObterUrl = "api/Departamentos/Obter";

        private string desafioApiUrl = "https://localhost:44388/"; // Endereço API IIS-Express

        public DepartamentosApiClient() : base()
        {
            //TODO
        }

        public List<DepartamentoViewModel> DepartamentosListar()
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var querys = default(Dictionary<string, object>); // Não há parâmetros para essa chamada

            var response = base.Get($"{desafioApiUrl}{departamentosListUrl}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<DepartamentoViewModel>>(json);
        }

        public bool DepartamentoGravar(DepartamentoViewModel departamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var response = base.Post($"{desafioApiUrl}{departamentosAdicionarUrl}", departamento, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        public bool DepartamentoAtualizar(int id, string descricao)
        {
            try
            {
                var data = new { descricao };
                var jsonData = JsonConvert.SerializeObject(data);
                var headers = new Dictionary<string, object>()
        {
            { "Content-Type", "application/json" }
        };

                var response = base.Put($"{desafioApiUrl}{departamentosAtualizarUrl}/{id}", jsonData, headers);
                base.EnsureSuccessStatusCode(response);
                string json = base.ReadHttpWebResponseMessage(response);
                var req = JsonConvert.DeserializeObject<bool>(json);

                return req;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar departamento: {ex.Message}");
                return false;
            }
        }



        public bool DepartamentoExcluir(int id)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var querys = new Dictionary<string, object>()
            {
                { "id", id }
            };

            var response = base.Delete($"{desafioApiUrl}{departamentosExcluirUrl}/{id}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        public DepartamentoViewModel DepartamentoObter(int id)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var querys = new Dictionary<string, object>()
            {
                { "id", id }
            };

            var response = base.Get($"{desafioApiUrl}{departamentosObterUrl}/{id}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<DepartamentoViewModel>(json);
        }
    }
}

