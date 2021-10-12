using System;

namespace SmartSchool.Dominio.Semestres
{
   public class Semestre
    {
        public Guid ID { get; private set; }
        public int Numero { get; private set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}
