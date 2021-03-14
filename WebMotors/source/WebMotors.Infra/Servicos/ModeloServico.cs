using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebMotors.Core.Entidades;
using WebMotors.Core.Servicos;

namespace WebMotors.Infra.Servicos
{
    public class ModeloServico : IModeloServico
    {
        private readonly HttpClient client;

        public ModeloServico(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("WebMotors:BaseUrl"));
            this.client = client;
        }

        public async Task<List<Modelo>> ObterModelosPorMarca(int marcaId)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/OnlineChallenge/Model?MakeID={marcaId}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<Modelo>>(await response.Content.ReadAsStringAsync(), options);
            }

            return null;
        }
    }
}
