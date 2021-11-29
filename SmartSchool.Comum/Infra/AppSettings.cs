using SmartSchool.Comum.Infra.Opcoes;
using System;

namespace SmartSchool.Comum.Infra
{
    public static class AppSettings
    {
        public static DataOpcoes Data { get; private set; }
       
        public static void SetarOpcoes(DataOpcoes dataOpcoes)
        {
            Data = dataOpcoes;            
        }
    }
}
