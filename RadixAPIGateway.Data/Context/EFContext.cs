using Microsoft.EntityFrameworkCore;
using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.Data.Context
{
    public class EFContext : DbContext
    {
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Acquirer> Acquirer { get; set; }
        
        public IConfigurationRoot Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStrings = GetConnectionStringsHelper.GetConnectionStrings();

            if (connectionStrings != null && connectionStrings.Count > 0)
            {
                var connStr = connectionStrings["PeopleNetConnectionString"];

                if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(connStr))
                {
                    optionsBuilder.UseSqlServer(connStr, b => b.UseRowNumberForPaging());
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aba>(entity =>
            {
                entity.ToTable("Aba", "FichaMedica");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ordem).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<AbaPerfil>(entity =>
            {
                entity.HasKey(e => new { e.IdAba, e.IdPerfil });

                entity.ToTable("AbaPerfil", "FichaMedica");

                entity.HasOne(d => d.Aba)
                    .WithMany(p => p.AbasPerfil)
                    .HasForeignKey(d => d.IdAba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AbaPerfil_Aba");
            });

            modelBuilder.Entity<AbaTipoExame>(entity =>
            {
                entity.HasKey(e => new { e.IdAba, e.IdTipoExame });

                entity.ToTable("AbaTipoExame", "FichaMedica");

                entity.HasOne(d => d.Aba)
                    .WithMany(p => p.AbasTipoExame)
                    .HasForeignKey(d => d.IdAba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AbaTipoExame_Aba");

                entity.HasOne(d => d.TipoExame)
                    .WithMany(p => p.AbasTipoExame)
                    .HasForeignKey(d => d.IdTipoExame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AbaTipoExame_TipoExame");
            });

            modelBuilder.Entity<AmbienteTrabalho>(entity =>
            {
                entity.ToTable("AmbienteTrabalho", "eSocial");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CNPJEstabelecimentoTerceiro)
                    .HasColumnName("CNPJEstabelecimentoTerceiro")
                    .HasColumnType("char(14)");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.AmbientesTrabalho)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AmbienteTrabalho_Empresa");
            });




            modelBuilder.Entity<Apresentacao>(entity =>
            {
                entity.ToTable("Apresentacao", "Sistema");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AssociacaoRisco>(entity =>
            {
                entity.ToTable("AssociacaoRisco", "Seguranca");
                entity.HasKey(e => e.IdAssociacaoRisco);

                entity.Property(e => e.IdAssociacaoRisco).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Caracterizado).HasDefaultValueSql("((1))");

                entity.Property(e => e.ObservacaoPpra)
                    .HasColumnName("ObservacaoPPRA")
                    .HasColumnType("text");

            });

            modelBuilder.Entity<Atividade>(entity =>
            {
                entity.ToTable("Atividade", "Estrutura");

                entity.HasIndex(e => new { e.IdEmpresa, e.Codigo })
                    .HasName("UK_Atividade_Codigo")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NomeTraduzido)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Atividades)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Atividade_Empresa");
            });

            modelBuilder.Entity<CampanhaVacinacao>(entity =>
            {
                entity.ToTable("CampanhaVacinacao", "Vacinacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoriaConselhoProfissional>(entity =>
            {
                entity.HasKey(e => new { e.IdCategoriaProfissional, e.IdConselhoProfissional });

                entity.ToTable("CategoriaConselhoProfissional", "Sistema");

                entity.HasOne(d => d.CategoriaProfissional)
                    .WithMany(p => p.CategoriasConselhoProfissional)
                    .HasForeignKey(d => d.IdCategoriaProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoriaConselhoProfissional_Categoria");

                entity.HasOne(d => d.ConselhoProfissional)
                    .WithMany(p => p.CategoriasConselhoProfissional)
                    .HasForeignKey(d => d.IdConselhoProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoriaConselhoProfissional_Conselho");
            });

            modelBuilder.Entity<CategoriaProfissional>(entity =>
            {
                entity.ToTable("CategoriaProfissional", "Sistema");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cbo>(entity =>
            {
                entity.ToTable("CBO", "Sistema");

                entity.Property(e => e.Atividade).IsUnicode(false);

                entity.Property(e => e.Familia)
                    .IsRequired()
                    .HasColumnType("char(4)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Ocupacao)
                    .IsRequired()
                    .HasColumnType("char(2)");

                entity.Property(e => e.Requisitos).IsUnicode(false);
            });

            modelBuilder.Entity<CentroCusto>(entity =>
            {
                entity.ToTable("CentroCusto", "Estrutura");

                entity.HasIndex(e => new { e.IdEmpresa, e.Codigo })
                    .HasName("UK_CentroCusto_Codigo")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.CentrosCusto)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CentroCusto_Empresa");
            });

            modelBuilder.Entity<Classificacao>(entity =>
            {
                entity.ToTable("Classificacao", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.ExibirNoAso).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClassificacaoDeficiencia>(entity =>
            {
                entity.ToTable("ClassificacaoDeficiencia", "Deficiencia");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo).HasColumnType("char(1)");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente", "Sistema");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cnae>(entity =>
            {
                entity.ToTable("CNAE", "Sistema");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Risco)
                    .WithMany(p => p.Cnaes)
                    .HasForeignKey(d => d.IdRisco)
                    .HasConstraintName("FK_CNAE_Risco");
            });


            modelBuilder.Entity<ConfiguracaoMedicamentoEmpresa>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdMedicamento });

                entity.ToTable("ConfiguracaoMedicamentoEmpresa", "Medicamento");

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.ConfiguracoesMedicamentosEmpresas)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfiguracaoMedicamentoEmpresa_Empresa");

                entity.HasOne(d => d.Medicamento)
                    .WithMany(p => p.ConfiguracoesMedicamentosEmpresas)
                    .HasForeignKey(d => d.IdMedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfiguracaoMedicamentoEmpresa_Medicamento");
            });

            modelBuilder.Entity<ConfiguracaoMedicamentoEstoque>(entity =>
            {
                entity.ToTable("ConfiguracaoMedicamentoEstoque", "Medicamento");

                entity.HasIndex(e => new { e.IdEstoque, e.IdMedicamento })
                    .HasName("UK_ConfiguracaoMedicamentoEstoque")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Estoque)
                    .WithMany(p => p.ConfiguracoesMedicamentosEstoques)
                    .HasForeignKey(d => d.IdEstoque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfiguracaoMedicamentoEstoque_Estoque");

                entity.HasOne(d => d.Medicamento)
                    .WithMany(p => p.ConfiguracoesMedicamentosEstoques)
                    .HasForeignKey(d => d.IdMedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfiguracaoMedicamentoEstoque_Medicamento");
            });

            modelBuilder.Entity<ConselhoProfissional>(entity =>
            {
                entity.ToTable("ConselhoProfissional", "Sistema");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contato>(entity =>
            {
                entity.ToTable("Contato", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Bairro)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasColumnType("char(8)");

                entity.Property(e => e.Complemento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PessoaContato)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rua)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TelefoneeSocial)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Municipio)
                    .WithMany(p => p.Contatos)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_Contato_Municipio");

                entity.HasOne(d => d.TipoLogradouro)
                    .WithMany(p => p.Contatos)
                    .HasForeignKey(d => d.IdTipoLogradouro)
                    .HasConstraintName("FK_Contato_TipoLogradouro");
            });

            modelBuilder.Entity<Credenciado>(entity =>
            {
                entity.ToTable("Credenciado", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasColumnType("char(14)");

                entity.Property(e => e.Horario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).IsUnicode(false);

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.Credenciados)
                    .HasForeignKey(d => d.IdContato)
                    .HasConstraintName("FK_Credenciado_Contato");
            });

            modelBuilder.Entity<CredenciadoGrupoExame>(entity =>
            {
                entity.HasKey(e => new { e.IdCredenciado, e.IdGrupoExame });

                entity.ToTable("CredenciadoGrupoExame", "Operacao");

                entity.Property(e => e.Valor).HasColumnType("numeric(6, 2)");

                entity.HasOne(d => d.Credenciado)
                    .WithMany(p => p.CredenciadosGrupoExames)
                    .HasForeignKey(d => d.IdCredenciado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CredenciadoGrupoExame_Credenciado");

                entity.HasOne(d => d.GrupoExame)
                    .WithMany(p => p.CredenciadoGrupoExame)
                    .HasForeignKey(d => d.IdGrupoExame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CredenciadoGrupoExame_GrupoExame");
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.ToTable("Documento", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Texto).HasColumnType("text");
            });

            modelBuilder.Entity<DocumentoFuncionario>(entity =>
            {
                entity.ToTable("DocumentoFuncionario", "Operacao");

                entity.HasIndex(e => e.IdFuncionario)
                    .HasName("UK_DocumentoFuncionario_Funcionario")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ctps).HasColumnName("CTPS");

                entity.Property(e => e.EmissaoCtps).HasColumnName("EmissaoCTPS");

                entity.Property(e => e.EmissaoRg).HasColumnName("EmissaoRG");

                entity.Property(e => e.IdEstadoEmissaoCtps).HasColumnName("IdEstadoEmissaoCTPS");

                entity.Property(e => e.IdEstadoEmissaoRg).HasColumnName("IdEstadoEmissaoRG");

                entity.Property(e => e.OrgaoEmissaoRg)
                    .HasColumnName("OrgaoEmissaoRG")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pispasep).HasColumnName("PISPASEP");

                entity.Property(e => e.Rg).HasColumnName("RG");

                entity.Property(e => e.SerieCtps).HasColumnName("SerieCTPS");

                entity.HasOne(d => d.EstadoEmissaoCtps)
                    .WithMany(p => p.EstadosEmissaoCtps)
                    .HasForeignKey(d => d.IdEstadoEmissaoCtps)
                    .HasConstraintName("FK_DocumentoFuncionario_EstadoCTPS");

                entity.HasOne(d => d.EstadoEmissaoRg)
                    .WithMany(p => p.EstadosEmissaoRg)
                    .HasForeignKey(d => d.IdEstadoEmissaoRg)
                    .HasConstraintName("FK_DocumentoFuncionario_EstadoRG");

                entity.HasOne(d => d.Funcionario)
                    .WithOne(p => p.DocumentoFuncionario)
                    .HasForeignKey<DocumentoFuncionario>(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentoFuncionario_Funcionario");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa", "Estrutura");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasColumnType("char(14)");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdCnae).HasColumnName("IdCNAE");

                entity.Property(e => e.InscricaoEstadual)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.InscricaoMunicipal)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NomeRepresentanteLegal)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).IsUnicode(false);

                entity.Property(e => e.Pispasep)
                    .HasColumnName("PISPASEP")
                    .HasColumnType("char(11)");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.TipoEstabelecimento).HasColumnType("char(1)");

                entity.HasOne(d => d.Cnae)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdCnae)
                    .HasConstraintName("FK_Empresa_CNAE");

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdContato)
                    .HasConstraintName("FK_Empresa_Contato");

                entity.HasOne(d => d.EmpresaMatriz)
                    .WithMany(p => p.Filiais)
                    .HasForeignKey(d => d.IdEmpresaMatriz)
                    .HasConstraintName("FK_Empresa_EmpresaMatriz");

                entity.HasOne(d => d.MedicoCoordenador)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdMedicoCoordenador)
                    .HasConstraintName("FK_Empresa_MedicoCoordenador");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empresa_Grupo");

                entity.HasOne(d => d.RelatorioAso)
                    .WithMany(p => p.EmpresasRelatorioAso)
                    .HasForeignKey(d => d.IdRelatorioAso)
                    .HasConstraintName("FK_Empresa_RelatorioAso");

                entity.HasOne(d => d.RelatorioAsoPreenchimento)
                    .WithMany(p => p.EmpresasRelatorioAsoPreenchimento)
                    .HasForeignKey(d => d.IdRelatorioAsoPreenchimento)
                    .HasConstraintName("FK_Empresa_RelatorioAsoPreenchimento");

                entity.HasOne(d => d.RelatorioFichaMedica)
                    .WithMany(p => p.EmpresasRelatorioFichaMedica)
                    .HasForeignKey(d => d.IdRelatorioFichaMedica)
                    .HasConstraintName("FK_Empresa_RelatorioFichaMedica");

                entity.HasOne(d => d.RelatorioFichaMedicaPreenchimento)
                    .WithMany(p => p.EmpresasRelatorioFichaMedicaPreenchimento)
                    .HasForeignKey(d => d.IdRelatorioFichaMedicaPreenchimento)
                    .HasConstraintName("FK_Empresa_RelatorioFichaMedicaPreenchimento");
            });

            modelBuilder.Entity<Epi>(entity =>
            {
                entity.ToTable("Epi", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Nome)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Uso)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Substituicao)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Higienizacao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Conservacao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reposicao)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TipoAtenuacao).HasColumnType("char(1)");

                entity.Property(e => e.MetodoAvaliacao)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Custo)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DataFabricacao)
                    .IsUnicode(false);

                entity.Property(e => e.DataImportacao)
                    .IsUnicode(false);

                entity.Property(e => e.DataHomologacao)
                    .IsUnicode(false);

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nrrsf)
                   .HasColumnName("NRRsf")
                   .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Vencimento)
                    .IsUnicode(false);

                entity.Property(e => e.Especificacao).IsUnicode(false);

                entity.HasOne(d => d.EquipamentoFabricante)
                      .WithMany(p => p.Epis)
                      .HasForeignKey(d => d.EpiFabricante)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EPI_EquipamentoFabricante");

                entity.HasOne(d => d.FornecedorEquipamentos)
                      .WithMany(p => p.Epis)
                      .HasForeignKey(d => d.FornecedorEpi)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EPI_FornecedorEpi");

            });

            modelBuilder.Entity<EPIEmpresa>(entity =>
            {
                entity.HasKey(e => new { e.IdEpi, e.IdEmpresa });

                entity.ToTable("EPIEmpresa", "Seguranca");

                entity.Property(e => e.IdEpi).HasColumnName("IdEpi");

                entity.Property(e => e.IdEmpresa).HasColumnName("IdEmpresa");
            });

            modelBuilder.Entity<EquipamentoFabricante>(entity =>
            {
                entity.ToTable("EquipamentoFabricante", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.TipoEquipamento).HasColumnType("char(1)");
            });

            modelBuilder.Entity<EquipamentoMedico>(entity =>
            {
                entity.ToTable("EquipamentoMedico", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.CodigoCertificacaoCalibragem)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroSerie)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).IsUnicode(false);
            });


            modelBuilder.Entity<EquipamentoFabricante>(entity =>
            {
                entity.ToTable("EquipamentoFabricante", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.TipoEquipamento).HasColumnType("char(1)");
            });

            modelBuilder.Entity<EquipamentoMedico>(entity =>
            {
                entity.ToTable("EquipamentoMedico", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.CodigoCertificacaoCalibragem)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroSerie)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).IsUnicode(false);
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.ToTable("Especialidade", "Sistema");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstabelecimentoPrioridade>(entity =>
            {
                entity.ToTable("EstabelecimentoPrioridade", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao).HasColumnType("text");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado", "Sistema");

                entity.HasIndex(e => e.Uf)
                    .HasName("UK_Estado_UF")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("UF")
                    .HasColumnType("char(2)");
            });


            modelBuilder.Entity<Estoque>(entity =>
            {
                entity.ToTable("Estoque", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Estoques)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estoque_Empresa");
            });

            modelBuilder.Entity<Estrutura>(entity =>
            {
                entity.ToTable("Estrutura", "Estrutura");

                entity.HasIndex(e => new { e.IdSetor, e.IdFuncao, e.IdAtividade })
                    .HasName("UK_Estrutura")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DescricaoAtividade).IsUnicode(false);

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Atividade)
                    .WithMany(p => p.Estruturas)
                    .HasForeignKey(d => d.IdAtividade)
                    .HasConstraintName("FK_Estrutura_Atividade");

                entity.HasOne(d => d.Funcao)
                    .WithMany(p => p.Estruturas)
                    .HasForeignKey(d => d.IdFuncao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estrutura_Funcao");

                entity.HasOne(d => d.Setor)
                    .WithMany(p => p.Estruturas)
                    .HasForeignKey(d => d.IdSetor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estrutura_Setor");
            });

            modelBuilder.Entity<EstruturaAmbienteTrabalho>(entity =>
            {
                entity.HasKey(e => new { e.IdEstrutura, e.IdAmbienteTrabalho, e.Inicio });

                entity.ToTable("EstruturaAmbienteTrabalho", "Estrutura");

                entity.HasOne(d => d.AmbienteTrabalho)
                    .WithMany(p => p.EstruturasAmbienteTrabalho)
                    .HasForeignKey(d => d.IdAmbienteTrabalho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstruturaAmbienteTrabalho_AmbienteTrabalho");

                entity.HasOne(d => d.Estrutura)
                    .WithMany(p => p.EstruturasAmbienteTrabalho)
                    .HasForeignKey(d => d.IdEstrutura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstruturaAmbienteTrabalho_Estrutura");
            });

            modelBuilder.Entity<Exame>(entity =>
            {
                entity.ToTable("Exame", "Saude");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Metodo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeExame)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Unidade)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoResultado)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.GrupoExame)
                  .WithMany(p => p.Exame)
                  .HasForeignKey(d => d.IdGrupoExame)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Exame_GrupoExame");
            });

            modelBuilder.Entity<ExposicaoRisco>(entity =>
            {
                entity.ToTable("ExposicaoRisco", "Seguranca");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descricao).HasColumnType("text");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FichaMedica>(entity =>
            {
                entity.ToTable("FichaMedica", "FichaMedica");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.JustificativaAlteracao)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Local)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Normal).HasDefaultValueSql("((1))");

                entity.Property(e => e.Observacao).IsUnicode(false);

                entity.Property(e => e.ObservacaoAso)
                    .HasColumnName("ObservacaoASO")
                    .IsUnicode(false);

                entity.HasOne(d => d.Funcionario)
                    .WithMany(p => p.FichasMedicas)
                    .HasForeignKey(d => d.IdFuncionario)
                    .HasConstraintName("FK_FichaMedica_Funcionario");

                entity.HasOne(d => d.HistoricoFuncionario)
                    .WithMany(p => p.FichasMedicas)
                    .HasForeignKey(d => d.IdHistoricoFuncionario)
                    .HasConstraintName("FK_FichaMedica_HistoricoFuncionario");

                entity.HasOne(d => d.MedicoExaminador)
                    .WithMany(p => p.FichasMedicas)
                    .HasForeignKey(d => d.IdMedicoExaminador)
                    .HasConstraintName("FK_FichaMedica_Profissional");

                entity.HasOne(d => d.TipoExame)
                    .WithMany(p => p.FichasMedicas)
                    .HasForeignKey(d => d.IdTipoExame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FichaMedica_TipoExame");

                entity.HasOne(d => d.TipoTrabalho)
                    .WithMany(p => p.FichasMedicas)
                    .HasForeignKey(d => d.IdTipoTrabalho)
                    .HasConstraintName("FK_FichaMedica_TipoTrabalho");
            });

            modelBuilder.Entity<FonteGeradora>(entity =>
            {
                entity.ToTable("FonteGeradora", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FornecedorEquipamentos>(entity =>
            {
                entity.ToTable("FornecedorEquipamentos", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).HasColumnType("text");

                entity.Property(e => e.TipoEquipamento).HasColumnType("char(1)");

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.FornecedorEquipamentos)
                    .HasForeignKey(d => d.IdContato)
                    .HasConstraintName("FK_Fornecedor_contato");
            });

            modelBuilder.Entity<FornecedorFabricanteEpi>(entity =>
            {
                entity.HasKey(e => new { e.IdFornecedorEpi, e.IdfabricanteEpi });

                entity.ToTable("FornecedorFabricanteEPI", "Seguranca");

                entity.Property(e => e.IdFornecedorEpi).HasColumnName("IdFornecedorEPI");

                //entity.HasOne(d => d.FornecedorEpi)
                //  .WithMany(p => p.Epis)
                //  .HasForeignKey(d => d.)
                //  .HasConstraintName("FK_IdFornecedorEPI");


                entity.Property(e => e.IdfabricanteEpi).HasColumnName("IDFabricanteEPI");
            });

            modelBuilder.Entity<Funcao>(entity =>
            {
                entity.ToTable("Funcao", "Estrutura");

                entity.HasIndex(e => new { e.IdEmpresa, e.Codigo })
                    .HasName("UK_Funcao_Codigo")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdCbo).HasColumnName("IdCBO");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeTraduzido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Requisitos).IsUnicode(false);

                entity.HasOne(d => d.Cbo)
                    .WithMany(p => p.Funcoes)
                    .HasForeignKey(d => d.IdCbo)
                    .HasConstraintName("FK_Funcao_CBO");

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Funcoes)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Funcao_Empresa");
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.ToTable("Funcionario", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF");

                entity.Property(e => e.FatorRh)
                    .HasColumnName("FatorRH")
                    .HasColumnType("char(1)");

                entity.Property(e => e.GrupoSanguineo).HasColumnType("char(2)");

                entity.Property(e => e.Nome).IsRequired();

                entity.Property(e => e.Observacao).IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdContato)
                    .HasConstraintName("FK_Funcionario_Contato");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.ToTable("Grupo", "Estrutura");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GrupoExame>(entity =>
            {
                entity.ToTable("GrupoExame", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeTraduzido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("numeric(6, 2)");
            });

            modelBuilder.Entity<HistoricoAmbienteTrabalho>(entity =>
            {
                entity.ToTable("HistoricoAmbienteTrabalho", "eSocial");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.AmbienteTrabalho)
                    .WithMany(p => p.HistoricoAmbienteTrabalho)
                    .HasForeignKey(d => d.IdAmbienteTrabalho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoAmbienteTrabalho_AmbienteTrabalho");
            });

            modelBuilder.Entity<HistoricoFuncionario>(entity =>
            {
                entity.ToTable("HistoricoFuncionario", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Estrutura)
                    .WithMany(p => p.HistoricosFuncionario)
                    .HasForeignKey(d => d.IdEstrutura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoFuncionario_Estrutura");

                entity.HasOne(d => d.Vinculo)
                    .WithMany(p => p.HistoricosFuncionario)
                    .HasForeignKey(d => d.IdVinculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoFuncionario_Vinculo");
            });


            modelBuilder.Entity<InstrumentoMedicao>(entity =>
            {
                entity.ToTable("InstrumentoMedicao", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).IsUnicode(false);
            });


            modelBuilder.Entity<Kit>(entity =>
            {
                entity.ToTable("Kit", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KitMedicamento>(entity =>
            {
                entity.ToTable("KitMedicamento", "Medicamento");

                entity.HasIndex(e => new { e.IdKit, e.IdMedicamento, e.IdEquipamentoMedico })
                    .HasName("UK_KitMedicamento")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.EquipamentoMedico)
                    .WithMany(p => p.KitsMedicamento)
                    .HasForeignKey(d => d.IdEquipamentoMedico)
                    .HasConstraintName("FK_KitMedicamento_EquipamentoMedico");

                entity.HasOne(d => d.Kit)
                    .WithMany(p => p.KitsMedicamento)
                    .HasForeignKey(d => d.IdKit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KitMedicamento_Kit");

                entity.HasOne(d => d.Medicamento)
                    .WithMany(p => p.KitsMedicamento)
                    .HasForeignKey(d => d.IdMedicamento)
                    .HasConstraintName("FK_KitMedicamento_Medicamento");
            });


            modelBuilder.Entity<Laboratorio>(entity =>
            {
                entity.ToTable("Laboratorio", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Local>(entity =>
            {
                entity.ToTable("Local", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Foto).IsUnicode(false);
            });


            modelBuilder.Entity<LoteMedicamento>(entity =>
            {
                entity.ToTable("LoteMedicamento", "Medicamento");

                entity.HasIndex(e => new { e.IdMedicamento, e.Numero })
                    .HasName("UK_LoteMedicamento_NumeroLote")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Medicamento)
                    .WithMany(p => p.LotesMedicamento)
                    .HasForeignKey(d => d.IdMedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoteMedicamento_Medicamento");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.ToTable("Medicamento", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).IsUnicode(false);

                entity.Property(e => e.Prescricao).IsUnicode(false);

                entity.Property(e => e.Substancia)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Apresentacao)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdApresentacao)
                    .HasConstraintName("FK_Medicamento_Apresentacao");

                entity.HasOne(d => d.Laboratorio)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdLaboratorio)
                    .HasConstraintName("FK_Medicamento_Laboratorio");

                entity.HasOne(d => d.Uso)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdUso)
                    .HasConstraintName("FK_Medicamento_Uso");

                entity.HasOne(d => d.Via)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdVia)
                    .HasConstraintName("FK_Medicamento_Via");
            });

            modelBuilder.Entity<MedidaControle>(entity =>
            {
                entity.ToTable("MedidaControle", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<MeioPropagacao>(entity =>
            {
                entity.ToTable("MeioPropagacao", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<MotivoMovimentacaoEstoque>(entity =>
            {
                entity.ToTable("MotivoMovimentacaoEstoque", "Sistema");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<MovimentacaoEstoque>(entity =>
            {
                entity.ToTable("MovimentacaoEstoque", "Medicamento");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Estoque)
                    .WithMany(p => p.MovimentacoesEstoque)
                    .HasForeignKey(d => d.IdEstoque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimentacaoEstoque_Estoque");

                entity.HasOne(d => d.LoteMedicamento)
                    .WithMany(p => p.MovimentacoesEstoque)
                    .HasForeignKey(d => d.IdLoteMedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimentacaoEstoque_LoteMedicamento");

                entity.HasOne(d => d.MotivoMovimentacaoEstoque)
                    .WithMany(p => p.MovimentacoesEstoque)
                    .HasForeignKey(d => d.IdMotivoMovimentacaoEstoque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimentacaoEstoque_MotivoMovimentacaoEstoque");
            });

            modelBuilder.Entity<MovimentacaoEstoqueSaida>(entity =>
            {
                entity.HasKey(e => e.IdMovimentacaoEstoque);

                entity.ToTable("MovimentacaoEstoqueSaida", "Medicamento");

                entity.Property(e => e.IdMovimentacaoEstoque).ValueGeneratedNever();

                entity.HasOne(d => d.Funcionario)
                    .WithMany(p => p.MovimentacoesEstoqueSaida)
                    .HasForeignKey(d => d.IdFuncionario)
                    .HasConstraintName("FK_MovimentacaoEstoqueSaida_Funcionario");

                entity.HasOne(d => d.MovimentacaoEstoque)
                    .WithOne(p => p.MovimentacaoEstoqueSaida)
                    .HasForeignKey<MovimentacaoEstoqueSaida>(d => d.IdMovimentacaoEstoque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimentacaoEstoqueSaida_MovimentacaoEstoque");

                entity.HasOne(d => d.ProfissionalEntrega)
                    .WithMany(p => p.MovimentacaoEstoqueSaidaProfissionalEntrega)
                    .HasForeignKey(d => d.IdProfissionalEntrega)
                    .HasConstraintName("FK_MovimentacaoEstoqueSaida_ProfissionalEntrega");

                entity.HasOne(d => d.ProfissionalPrescricao)
                    .WithMany(p => p.MovimentacaoEstoqueSaidaProfissionalPrescricao)
                    .HasForeignKey(d => d.IdProfissionalPrescricao)
                    .HasConstraintName("FK_MovimentacaoEstoqueSaida_ProfissionalPrescricao");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.ToTable("Municipio", "Sistema");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipio_Estado");
            });


            modelBuilder.Entity<Pergunta>(entity =>
            {
                entity.ToTable("Pergunta", "FichaMedica");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FormatoTexto)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ordem).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("('A')");

                entity.Property(e => e.TipoControle)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.HasOne(d => d.Aba)
                    .WithMany(p => p.Perguntas)
                    .HasForeignKey(d => d.IdAba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pergunta_Aba");

                entity.HasOne(p => p.Resposta)
                    .WithMany(r => r.PerguntasAssociadas)
                    .HasForeignKey(p => p.IdResposta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pergunta_Resposta");
            });

            modelBuilder.Entity<Procedimento>(entity =>
            {
                entity.ToTable("Procedimento", "Saude");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Profissional>(entity =>
            {
                entity.ToTable("Profissional", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Assinatura).IsUnicode(false);

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CPF)
                    .HasColumnName("CPF")
                    .HasColumnType("char(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeClinicaOfensora)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroConselhoProfissional)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroRegistroMTE)
                    .HasColumnName("NumeroRegistroMTE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao).IsUnicode(false);

                entity.Property(e => e.PISPASEP)
                    .HasColumnName("PISPASEP")
                    .HasColumnType("char(11)");

                entity.HasOne(d => d.CategoriaProfissional)
                    .WithMany(p => p.Profissionais)
                    .HasForeignKey(d => d.IdCategoriaProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profissional_Categoria");

                entity.HasOne(d => d.ConselhoProfissional)
                    .WithMany(p => p.Profissionais)
                    .HasForeignKey(d => d.IdConselhoProfissional)
                    .HasConstraintName("FK_Profissional_Conselho");

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.Profissionais)
                    .HasForeignKey(d => d.IdContato)
                    .HasConstraintName("FK_Profissional_Contato");
            });

            modelBuilder.Entity<ProgramacaoExame>(entity =>
            {
                entity.ToTable("ProgramacaoExame", "Saude");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.EnviarEsocial)
                    .HasColumnName("EnviarESocial")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Observacao).HasColumnType("text");

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.HasOne(d => d.GrupoExame)
                    .WithMany(p => p.ProgramacaoExame)
                    .HasForeignKey(d => d.IdGrupoExame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramacaoExame_GrupoExame");

                entity.HasOne(d => d.HistoricoAmbienteTrabalho)
                    .WithMany(p => p.ProgramacaoExame)
                    .HasForeignKey(d => d.IdHistoricoAmbienteTrabalho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramacaoExame_HistoricoAmbienteTrabalho");
            });

            modelBuilder.Entity<Queixa>(entity =>
            {
                entity.ToTable("Queixa", "Saude");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Regiao>(entity =>
            {
                entity.ToTable("Regiao", "Estrutura");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<RelatorioAsoFichaMedica>(entity =>
            {
                entity.ToTable("RelatorioAsoFichaMedica", "Sistema");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeClasseRelatorio)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ResponsaveisSeguranca>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdProfissional, e.Responsabilidade });

                entity.ToTable("ResponsaveisSeguranca", "Operacao");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Responsabilidade).HasColumnType("char(1)");

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.ResponsaveisSeguranca)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResponsaveisSeguranca_Empresa");

                entity.HasOne(d => d.Profissional)
                    .WithMany(p => p.ResponsaveisSeguranca)
                    .HasForeignKey(d => d.IdProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResponsaveisSeguranca_Profissional");
            });

            modelBuilder.Entity<Resposta>(entity =>
            {
                entity.ToTable("Resposta", "FichaMedica");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ordem).HasDefaultValueSql("((0))");

                entity.Property(e => e.TipoControle)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.HasOne(d => d.Pergunta)
                    .WithMany(p => p.Respostas)
                    .HasForeignKey(d => d.IdPergunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resposta_Pergunta");

                entity.HasMany(r => r.PerguntasAssociadas)
                    .WithOne(p => p.Resposta)
                    .IsRequired();
            });

            modelBuilder.Entity<Risco>(entity =>
            {
                entity.ToTable("Risco", "Sistema");

                entity.Property(e => e.Nome)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NomeTraduzido)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RiscoSaude>(entity =>
            {
                entity.ToTable("RiscoSaude", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.ToTable("Servico", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Setor>(entity =>
            {
                entity.ToTable("Setor", "Estrutura");

                entity.HasIndex(e => new { e.IdEmpresa, e.Codigo })
                    .HasName("UK_Setor_Codigo")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdRelatorioAso)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdRelatorioAsoPreenchimento)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdRelatorioFichaMedica)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdRelatorioFichaMedicaPreenchimento)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Setores)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Setor_Empresa");

            });

            modelBuilder.Entity<SituacaoFuncionario>(entity =>
            {
                entity.ToTable("SituacaoFuncionario", "Sistema");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TecnicaMedicao>(entity =>
            {
                entity.ToTable("TecnicaMedicao", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenciaLegalNormativa).IsUnicode(false);
            });

            modelBuilder.Entity<TipoAfastamento>(entity =>
            {
                entity.ToTable("TipoAfastamento", "Absenteismo");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoExame>(entity =>
            {
                entity.ToTable("TipoExame", "FichaMedica");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.CodigoeSocial)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<TipoLogradouro>(entity =>
            {
                entity.ToTable("TipoLogradouro", "Sistema");

                entity.HasIndex(e => e.Codigo)
                    .HasName("UK_TipoLogradouro")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoTrabalho>(entity =>
            {
                entity.ToTable("TipoTrabalho", "FichaMedica");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trajetoria>(entity =>
            {
                entity.ToTable("Trajetoria", "Seguranca");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Turnos>(entity =>
            {
                entity.ToTable("Turnos", "Estrutura");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Uso>(entity =>
            {
                entity.ToTable("Uso", "Sistema");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioEmpresa>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdEmpresa });

                entity.ToTable("UsuarioEmpresa", "Estrutura");

                entity.Property(e => e.Padrao).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.UsuariosEmpresas)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioEmpresa_Empresa");
            });

            modelBuilder.Entity<Via>(entity =>
            {
                entity.ToTable("Via", "Sistema");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Vinculo>(entity =>
            {
                entity.ToTable("Vinculo", "Operacao");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Matricula).IsRequired();

                entity.HasOne(d => d.Funcionario)
                    .WithMany(p => p.Vinculos)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vinculo_Funcionario");

                entity.HasOne(d => d.SituacaoFuncionario)
                    .WithMany(p => p.Vinculos)
                    .HasForeignKey(d => d.IdSituacaoFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vinculo_SituacaoFuncionario");
            });

            modelBuilder.Entity<VinculoAmbienteTrabalho>(entity =>
            {
                entity.HasKey(e => new { e.IdVinculo, e.IdAmbienteTrabalho, e.Inicio });

                entity.ToTable("VinculoAmbienteTrabalho", "Operacao");

                entity.HasOne(d => d.AmbienteTrabalho)
                    .WithMany(p => p.VinculosAmbienteTrabalho)
                    .HasForeignKey(d => d.IdAmbienteTrabalho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VinculoAmbienteTrabalho_AmbienteTrabalho");

                entity.HasOne(d => d.Vinculo)
                    .WithMany(p => p.VinculosAmbienteTrabalho)
                    .HasForeignKey(d => d.IdVinculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VinculoAmbienteTrabalho_Vinculo");
            });

        }
    }
}
