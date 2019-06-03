using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PoolLightPie.Services.Interfaces;

namespace PoolLightPie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EauController : ControllerBase
    {
        /// <summary>
        /// Services injectés.
        /// </summary>
        private readonly IServiceLectureTemperature _lecteurTemperature;
        private readonly IServiceLecturePh _lecteurPh;

        /// <summary>
        /// Constructeur par injection.
        /// </summary>
        /// <param name="lecteurTemperature">Lecteur de la température de l'eau.</param>
        /// <param name="lecteurPh">Lecteur du pH.</param>
        public EauController(IServiceLectureTemperature lecteurTemperature, IServiceLecturePh lecteurPh)
        {
            _lecteurTemperature = lecteurTemperature;
            _lecteurPh = lecteurPh;
        }

        /// <summary>
        /// Obtenir les informations de l'eau (temperature).
        /// </summary>
        /// <returns>Température.</returns>
        [HttpGet("temperature")]
        public IActionResult ObtenirTemperature() =>
            Ok(_lecteurTemperature.Lire());

        /// <summary>
        /// Obtenir les informations de l'eau (pH).
        /// </summary>
        /// <returns>pH.</returns>
        [HttpGet("pH")]
        public IActionResult ObtenirPh() =>
            Ok(_lecteurPh.Lire());
    }
}
