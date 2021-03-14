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
    public class MarcaServico : IMarcaServico
    {
        private readonly HttpClient client;

        public MarcaServico(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("WebMotors:BaseUrl"));
            this.client = client;
        }

        public async Task<List<Marca>> ObterMarcas()
        {
            HttpResponseMessage response = await client.GetAsync("/api/OnlineChallenge/Make");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<Marca>>(await response.Content.ReadAsStringAsync(), options);
            }

            return null;
        }
    }
}
