using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Clientes.Domain.Models.Procedures
{
    public class SpsBuscarTablaCatalogoResult
    {
        [Column("JsonData")]
        public string JsonData { get; set; } = string.Empty;
    }
}
