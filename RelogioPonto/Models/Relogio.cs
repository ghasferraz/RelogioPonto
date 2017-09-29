using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RelogioPonto.Models
{
	public class Relogio
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Nome { get; set; }
		public string Descricao { get; set; }

        public StatusPapel Status { get; set; }

        [Required]
		public string Login { get; set; }
		[Required]
		public string Senha { get; set; }
		[Required]
		public string Ip { get; set; }


        public enum StatusPapel
        {
            Baixo, Regular, Alto, Cheio
        }
    }
}