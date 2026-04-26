using ApiProcessamento.Config;
using ApiProcessamento.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared;

namespace ApiProcessamento.Controllers
{
    /// <summary>
    /// Controller responsável pelo recebimento, validação,
    /// armazenamento e consulta de sinais industriais.
    /// </summary>
    /// <remarks>
    /// Esta API simula um sistema de monitoramento industrial,
    /// recebendo dados de sensores como temperatura (°C) e corrente elétrica (A),
    /// validando limites operacionais e persistindo em banco de dados SQLite.
    /// </remarks>
    [ApiController]
    [Route("api/v1/sensores")]
    public class SensorController : ControllerBase
    {
        /// <summary>
        /// Configurações da API contendo limites de operação dos sensores
        /// </summary>
        private readonly IOptions<ApiConfig> _config;

        /// <summary>
        /// Contexto do banco de dados responsável pela persistência dos dados
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor da classe com injeção de dependência
        /// </summary>
        /// <param name="config">Configurações da API (limites de temperatura e corrente)</param>
        /// <param name="context">Contexto do banco de dados SQLite</param>
        public SensorController(IOptions<ApiConfig> config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        /// <summary>
        /// Recebe um novo sinal industrial do sistema de simulação
        /// </summary>
        /// <param name="sensor">
        /// Objeto contendo os dados do sensor, incluindo:
        /// temperatura (°C), corrente elétrica (A) e timestamp
        /// </param>
        /// <returns>
        /// Retorna o sensor salvo em caso de sucesso ou uma mensagem de erro
        /// caso os valores ultrapassem os limites permitidos
        /// </returns>
        /// <remarks>
        /// Regras de validação:
        /// - Temperatura não pode ultrapassar o valor máximo definido em configuração
        /// - Corrente elétrica não pode ultrapassar o valor máximo definido
        /// 
        /// Caso os dados sejam válidos, o registro é persistido no banco SQLite.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Receber([FromBody] SensorData sensor)
        {
            if (sensor.Temperatura > _config.Value.MaxTemperatura)
                return BadRequest("Temperatura acima do limite permitido!");

            if (sensor.Corrente > _config.Value.MaxCorrente)
                return BadRequest("Corrente acima do limite permitido!");

            sensor.Timestamp = DateTime.Now;

            await _context.Sensores.AddAsync(sensor);
            await _context.SaveChangesAsync();

            return Ok(sensor);
        }

        /// <summary>
        /// Retorna todos os sinais industriais registrados no sistema
        /// </summary>
        /// <returns>
        /// Lista de sensores com dados formatados contendo:
        /// ID, temperatura (°C), corrente (A) e timestamp
        /// </returns>
        /// <remarks>
        /// Os dados retornados são formatados para melhor visualização,
        /// adicionando unidades de medida:
        /// - Temperatura em graus Celsius (°C)
        /// - Corrente elétrica em Ampere (A)
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var dados = await _context.Sensores.ToListAsync();

            //Transforma (formata) os dados adicionando as letras de unidade
            var dadosFormatados = dados.Select(s => new
            {
                id = s.Id,
                temperatura = $"{s.Temperatura} ºC",
                corrente = $"{s.Corrente} A",
                timestamp = s.Timestamp
            });

            return Ok(dadosFormatados);
        }
    }
}