﻿using FluentValidation;
using SmartSchool.Dto.Professores;
using System;

namespace SmartSchool.Dominio.Professores.Validacao
{
	public class ProfessorValidacao : AbstractValidator<ProfessorDto>
	{
		public ProfessorValidacao()
		{
			this.RuleFor(x => x.Nome).NotEmpty()
				.WithMessage("Nome do Professor deve ser informado.");

			this.RuleFor(x => x.Matricula).NotEmpty()
				.WithMessage("Matrícula de Professor deve ser informada.");

			this.RuleFor(p => p.Nome)
				.MaximumLength(32).WithMessage("Nome do Professor não pode passar de 32 caracteres.");

			this.RuleFor(x => x.Disciplinas).NotEmpty()
				.WithMessage("Deve ser informado ao menos uma Disciplina.");
		}
	}
}