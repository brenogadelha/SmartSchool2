﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartSchool.Dados.Contextos;

namespace SmartSchool.Dados.Migrations
{
    [DbContext(typeof(SmartContexto))]
    [Migration("20211107032744_AlteracaoAluno")]
    partial class AlteracaoAluno
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SmartSchool")
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartSchool.Dominio.Alunos.Aluno", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ALUN_ID_ALUNO")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnName("ALUN_IN_ATIVO")
                        .HasColumnType("bit");

                    b.Property<string>("Celular")
                        .HasColumnName("ALUN_NR_CELULAR")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<string>("Cidade")
                        .HasColumnName("ALUN_TXT_CIDADE")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnName("ALUN_NR_CPF")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<Guid>("CursoId")
                        .HasColumnName("ALUN_ID_CURSO")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataFim")
                        .HasColumnName("ALUN_DT_FIM")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnName("ALUN_DT_INICIO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnName("ALUN_DT_NASCIMENTO")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("ALUN_TXT_EMAIL")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("Matricula")
                        .HasColumnName("ALUN_COD_ALUNO")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("ALUN_NM_NOME")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnName("ALUN_NM_SOBRENOME")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Telefone")
                        .HasColumnName("ALUN_NR_TELEFONE")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.HasKey("ID");

                    b.HasIndex("CursoId");

                    b.ToTable("ALUNO");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Alunos.AlunoDisciplina", b =>
                {
                    b.Property<Guid>("AlunoID")
                        .HasColumnName("ALDI_ID_ALUNO")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DisciplinaID")
                        .HasColumnName("ALDI_ID_DISCIPLINA")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlunoID", "DisciplinaID");

                    b.HasIndex("DisciplinaID");

                    b.ToTable("ALUNO_DISCIPLINA");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Cursos.Curso", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CURS_ID_CURSO")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("CURS_NM_CURSO")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.ToTable("CURSO");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Disciplinas.CursoDisciplina", b =>
                {
                    b.Property<Guid>("CursoID")
                        .HasColumnName("CUDI_ID_CURSO")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DisciplinaID")
                        .HasColumnName("CUDI_ID_DISCIPLINA")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CursoID", "DisciplinaID");

                    b.HasIndex("DisciplinaID");

                    b.ToTable("CURSO_DISCIPLINA");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Disciplinas.Disciplina", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DISC_ID_DISCIPLINA")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("DISC_NM_NOME")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int>("Periodo")
                        .HasColumnName("DISC_ID_PERIODO")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("DISCIPLINA");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Professores.Professor", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnName("PROF_ID_PROFESSOR")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Matricula")
                        .HasColumnName("PROF_COD_PROFESSOR")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("PROF_NM_NOME")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.ToTable("PROFESSOR");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Professores.ProfessorDisciplina", b =>
                {
                    b.Property<Guid>("ProfessorID")
                        .HasColumnName("PRDI_ID_PROFESSOR")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DisciplinaID")
                        .HasColumnName("PRDI_ID_DISCIPLINA")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProfessorID", "DisciplinaID");

                    b.HasIndex("DisciplinaID");

                    b.ToTable("PROFESSOR_DISCIPLINA");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Semestres.Semestre", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SEME_ID_SEMESTRE")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataFim")
                        .HasColumnName("SEME_DT_FIM")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnName("SEME_DT_INICIO")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("SEMESTRE");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Semestres.SemestreAlunoDisciplina", b =>
                {
                    b.Property<Guid>("SemestreID")
                        .HasColumnName("SEAD_ID_SEMESTRE")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlunoID")
                        .HasColumnName("SEAD_ID_ALUNO")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DisciplinaID")
                        .HasColumnName("SEAD_ID_DISCIPLINA")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Periodo")
                        .HasColumnName("SEAD_ID_PERIODO")
                        .HasColumnType("int");

                    b.Property<int>("StatusDisciplina")
                        .HasColumnName("SEAD_ID_STATUS_DISCIPLINA")
                        .HasColumnType("int");

                    b.HasKey("SemestreID", "AlunoID", "DisciplinaID");

                    b.HasIndex("AlunoID", "DisciplinaID");

                    b.ToTable("SEMESTRE_ALUNO_DISCIPLINA");
                });

            modelBuilder.Entity("SmartSchool.Dominio.Alunos.Aluno", b =>
                {
                    b.HasOne("SmartSchool.Dominio.Cursos.Curso", "Curso")
                        .WithMany("Alunos")
                        .HasForeignKey("CursoId")
                        .HasConstraintName("FK_ALUN_CURSO")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartSchool.Dominio.Alunos.AlunoDisciplina", b =>
                {
                    b.HasOne("SmartSchool.Dominio.Alunos.Aluno", "Aluno")
                        .WithMany("AlunosDisciplinas")
                        .HasForeignKey("AlunoID")
                        .HasConstraintName("FK_ALUN_DISC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartSchool.Dominio.Disciplinas.Disciplina", "Disciplina")
                        .WithMany("Alunos")
                        .HasForeignKey("DisciplinaID")
                        .HasConstraintName("FK_DISC_ALUN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartSchool.Dominio.Disciplinas.CursoDisciplina", b =>
                {
                    b.HasOne("SmartSchool.Dominio.Cursos.Curso", "Curso")
                        .WithMany("CursosDisciplinas")
                        .HasForeignKey("CursoID")
                        .HasConstraintName("FK_CURS_DISC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartSchool.Dominio.Disciplinas.Disciplina", "Disciplina")
                        .WithMany("Cursos")
                        .HasForeignKey("DisciplinaID")
                        .HasConstraintName("FK_DISC_CURS")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartSchool.Dominio.Professores.ProfessorDisciplina", b =>
                {
                    b.HasOne("SmartSchool.Dominio.Disciplinas.Disciplina", "Disciplina")
                        .WithMany("ProfessoresDisciplinas")
                        .HasForeignKey("DisciplinaID")
                        .HasConstraintName("FK_DISC_PROF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartSchool.Dominio.Professores.Professor", "Professor")
                        .WithMany("ProfessoresDisciplinas")
                        .HasForeignKey("ProfessorID")
                        .HasConstraintName("FK_PROF_DISC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartSchool.Dominio.Semestres.SemestreAlunoDisciplina", b =>
                {
                    b.HasOne("SmartSchool.Dominio.Alunos.Aluno", null)
                        .WithMany("SemestresDisciplinas")
                        .HasForeignKey("AlunoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartSchool.Dominio.Semestres.Semestre", "Semestre")
                        .WithMany("AlunosDisciplinas")
                        .HasForeignKey("SemestreID")
                        .HasConstraintName("FK_SEME_ALDI")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartSchool.Dominio.Alunos.AlunoDisciplina", "AlunoDisciplina")
                        .WithMany("Semestres")
                        .HasForeignKey("AlunoID", "DisciplinaID")
                        .HasConstraintName("FK_ALDI_SEME")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}