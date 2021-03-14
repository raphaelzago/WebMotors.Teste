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
    public class VersaoServico : IVersaoServico
    {
        private readonly HttpClient client;

        public VersaoServico(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("WebMotors:BaseUrl"));
            this.client = client;
        }

        public async Task<List<Versao>> ObterVersoesPorModelo(int modeloId)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/OnlineChallenge/Version?ModelID={modeloId}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<Versao>>(await response.Content.ReadAsStringAsync(), options);
            }

            return null;
        }
    }
}
