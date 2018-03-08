using Dapper.FluentMap.Dommel.Mapping;
using CicloVidaAltoValor.Application.Model.Entities;

namespace CicloVidaAltoValor.Application.Repository.Mappings
{
    public class ParceiroMap : DommelEntityMap<Parceiro>
    {
        public ParceiroMap()
        {
            ToTable("DBS_PARCEIRO");

            Map(t => t.UsuarioIdParceiro)
                .ToColumn("USUARIO_ID_PARCEIRO")
                .IsIdentity()
                .IsKey();

            Map(x => x.Nome)
                .ToColumn("NOME");


            Map(x => x.Ativo)
                .ToColumn("ATIVO");

            Map(x => x.BusinessId)
                .ToColumn("BUSINESS_ID");

            Map(p => p.ParceiroCategoriaId)
                .ToColumn("PARCEIRO_CATEGORIA_ID");

            Map(p => p.NomeFantasia)
                .ToColumn("NOME_FANTASIA");

            Map(p => p.ResponsavelMkt)
                .ToColumn("RESPONSAVEL_MKT");

            Map(p => p.ResponsavelGerente)
               .ToColumn("RESPONSAVEL_GERENTE");

            Map(p => p.ParceiroNegocioId)
                .ToColumn("PARCEIRO_NEGOCIO_ID");

            Map(p => p.PareiroPosPago)
                .ToColumn("PARCEIRO_POS_PAGO");

            Map(p => p.DiaFechamento)
                .ToColumn("DIA_FECHAMENTO");

            Map(p => p.ModeloCobrancaId)
                .ToColumn("MODELO_COBRANCA_ID");

            Map(p => p.ValidarFuncionarioPos)
                .ToColumn("VALIDAR_FUNCIONARIO_POS");

            Map(p => p.ControlarEstorno)
                .ToColumn("CONTROLAR_ESTORNO");

            Map(p => p.MecanicaIdDefaultBonus)
                .ToColumn("MECANICA_ID_DEFAULT_BONUS");

            Map(p => p.MecanicaIdDefaultBase)
                .ToColumn("MECANICA_ID_DEFAULT_BASE");

            Map(p => p.FlagExigeTransacaoParaCadastro)
                .ToColumn("FLG_EXIGE_TRANSACAO_PARA_CADASTRO");

            Map(p => p.MecanicaIdExigeTransacaoParaCadastro)
                .ToColumn("MECANICA_ID_EXIGE_TRANSACAO_PARA_CADASTRO");

            Map(p => p.DataDesativacao)
                .ToColumn("DATA_DESATIVACAO");

            Map(p => p.FlagCriarFaturaAutomatica)
                .ToColumn("FLAG_CRIAR_FATURA_AUTOMATICA");

            Map(p => p.ResponsavelPraca)
                .ToColumn("RESPONSAVEL_PRACA");

            Map(p => p.WmsFornecedorId)
                .ToColumn("WMS_FORNECEDOR_ID");

            Map(p => p.Cnae)
                .ToColumn("CNAE");

            //Map(p => p.DataHoraAlteracao).Ignore();
            //Map(p => p.DataHoraCadastro).Ignore();

        }
    }
}
