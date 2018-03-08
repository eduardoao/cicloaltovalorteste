using CicloVidaAltoValor.Application.Model.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("CPR_USUARIO");

            Map(p => p.UsuarioId).ToColumn("CPR_USUARIO_ID").IsKey().IsIdentity(); ;
            Map(p => p.CampanhaId).ToColumn("CPR_CAMPANHA_ID");
            Map(p => p.UsrUsuarioId).ToColumn("USR_USUARIO_ID");
            Map(p => p.DataOptin).ToColumn("DT_OPT_IN");
            Map(p => p.Nivel).ToColumn("NIVEL");
            Map(p => p.NivelCompartilhado).ToColumn("NIVEL_COMPARTILHADO");
            Map(p => p.Apelido).ToColumn("APELIDO");
            Map(p => p.Nome).ToColumn("NOME");
            Map(p => p.DataNascimento).ToColumn("DT_NASCIMENTO");
            Map(p => p.Documento).ToColumn("DOCUMENTO");
            Map(p => p.Email).ToColumn("EMAIL");
            Map(p => p.Logradouro).ToColumn("DESC_LOGRADOURO");
            Map(p => p.NumeroLogradouro).ToColumn("NUMERO_LOGRADOURO");
            Map(p => p.Estado).ToColumn("USR_ESTADO_UF");
            Map(p => p.Cep).ToColumn("NUMERO_CEP");
            Map(p => p.Cidade).ToColumn("NOME_CIDADE");
            Map(p => p.Bairro).ToColumn("NOME_BAIRRO");
            Map(p => p.Complemento).ToColumn("DESC_COMPLEMENTO");
            Map(p => p.InformacoesAdicionais).ToColumn("INFORMACOES_ADICIONAIS");
            Map(p => p.DddResidencial).ToColumn("NUMERO_DDD1");
            Map(p => p.TelefoneResidencial).ToColumn("NUMERO_TELEFONE1");
            Map(p => p.DddCelular).ToColumn("NUMERO_DDD2");
            Map(p => p.TelefoneCelular).ToColumn("NUMERO_TELEFONE2");
            Map(p => p.DddComercial).ToColumn("NUMERO_DDD3");
            Map(p => p.TelefoneComercial).ToColumn("NUMERO_TELEFONE3");
            Map(p => p.Sexo).ToColumn("INDICADOR_SEXO");
            Map(p => p.DataUltimaAtualizacao).ToColumn("DT_ULTIMA_ATUALIZACAO");
            Map(p => p.Observacao).ToColumn("OBSERVACAO");
            Map(p => p.UsuarioOrigemId).ToColumn("CPR_USUARIO_ORIGEM_ID");
            Map(p => p.Carteira).ToColumn("CARTEIRA");
            Map(p => p.DataOptOut).ToColumn("DT_OPT_OUT");
        }
    }
}
