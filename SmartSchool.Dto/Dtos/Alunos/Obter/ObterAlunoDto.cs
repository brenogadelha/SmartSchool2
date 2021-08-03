using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Dto.Dtos.Alunos.Obter
{
    public class ObterAlunoDto
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}
