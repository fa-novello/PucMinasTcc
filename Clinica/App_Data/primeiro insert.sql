SET IDENTITY_INSERT [dbo].[Pessoas] ON
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (1, N'Paciente 1', N'1999-07-04 00:00:00', N'111.111.111-11', N'(12)99999-9999', N'paciente1@teste.com', 2, N'12345', N'pac1', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'Paciente')
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (2, N'Paciente 2', N'1996-06-09 00:00:00', N'222.222.222-22', N'(12)99999-9999', N'paciente2@teste.com', 2, N'12345', N'pac2', NULL, NULL, NULL, NULL, NULL, 1, N'Unimed', N'2019-11-29 00:00:00', NULL, N'111 222 333', N'Paciente')
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (3, N'Medico 1', N'1982-06-29 00:00:00', N'333.333.333-33', N'(12)99999-9999', N'medico1@teste.com', 1, N'12345', N'med1', 1, N'111111', N'SP', 1, N'Anestesiologia', NULL, NULL, NULL, NULL, NULL, N'Medico')
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (4, N'Medico 2', N'1984-06-29 00:00:00', N'444.444.444-44', N'(12)99999-9999', N'medico2@teste.com', 1, N'12345', N'med2', 1, N'22222', N'SP', 2, N'Cardiologia', NULL, NULL, NULL, NULL, NULL, N'Medico')
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (5, N'Medico 3', N'1989-06-29 00:00:00', N'444.444.444-55', N'(12)99999-9999', N'medico3@teste.com', 1, N'12345', N'med3', 1, N'33333', N'SP', 4, N'Cirurgia pediatrica', NULL, NULL, NULL, NULL, NULL, N'Medico')
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (6, N'Laboratório 1', N'1994-07-04 00:00:00', N'111.222.111-11', N'(12)99999-9999', N'laboratorio1@teste.com', 4, N'12345', N'lab1', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Colaborador')
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (7, N'Colaborador 1', N'1999-07-24 00:00:00', N'111.333.111-11', N'(12)99999-9999', N'colaborador1@teste.com', 3, N'12345', N'colab1', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Colaborador')
INSERT INTO [dbo].[Pessoas] ([Id], [Nome], [Nascimento], [CPF], [Telefone], [Email], [Perfil], [Senha], [Login], [Ativo], [CRMNumero], [CRMEmissor], [EspecialidadeId], [EspecialidadeDescricao], [PossuiConvenio], [EmpresaConvenio], [ValidadeConvenio], [NumeroCarteiraConvenio], [NumeroCNS], [Discriminator]) VALUES (8, N'Colaborador 2', N'1997-07-14 00:00:00', N'111.333.111-11', N'(12)99999-9999', N'colaborador2@teste.com', 3, N'12345', N'colab2', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Colaborador')
SET IDENTITY_INSERT [dbo].[Pessoas] OFF


SET IDENTITY_INSERT [dbo].[AgendaConfiguracaos] ON
INSERT INTO [dbo].[AgendaConfiguracaos] ([Id], [MedicoId], [Medico], [DiaSemana], [LimiteConsultas], [TempoConsulta], [HorarioInicio]) VALUES (1, 3, N'Medico 1', 1, 5, 20, N'2019-06-29 09:00:00')
INSERT INTO [dbo].[AgendaConfiguracaos] ([Id], [MedicoId], [Medico], [DiaSemana], [LimiteConsultas], [TempoConsulta], [HorarioInicio]) VALUES (2, 3, N'Medico 1', 3, 5, 20, N'2019-06-29 09:15:00')
INSERT INTO [dbo].[AgendaConfiguracaos] ([Id], [MedicoId], [Medico], [DiaSemana], [LimiteConsultas], [TempoConsulta], [HorarioInicio]) VALUES (3, 4, N'Medico 2', 2, 7, 15, N'2019-06-29 09:30:00')
INSERT INTO [dbo].[AgendaConfiguracaos] ([Id], [MedicoId], [Medico], [DiaSemana], [LimiteConsultas], [TempoConsulta], [HorarioInicio]) VALUES (4, 4, N'Medico 2', 4, 7, 15, N'2019-06-29 10:00:00')
SET IDENTITY_INSERT [dbo].[AgendaConfiguracaos] OFF


SET IDENTITY_INSERT [dbo].[Agenda] ON
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (1, 3, NULL, NULL, NULL, NULL, N'2019-06-29 13:00:00', N'2019-06-29 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (2, 3, NULL, NULL, NULL, NULL, N'2019-06-29 13:20:00', N'2019-06-29 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (3, 3, NULL, NULL, NULL, NULL, N'2019-06-29 14:00:00', N'2019-06-29 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (4, 3, 1, NULL, NULL, NULL, N'2019-06-29 16:00:00', N'2019-06-29 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (5, 3, NULL, NULL, NULL, NULL, N'2019-06-29 15:00:00', N'2019-06-29 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (6, 4, NULL, NULL, NULL, NULL, N'2019-06-29 13:20:00', N'2019-07-04 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (7, 4, NULL, NULL, NULL, NULL, N'2019-06-29 15:20:00', N'2019-06-29 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (8, 4, NULL, NULL, NULL, NULL, N'2019-06-29 16:20:00', N'2019-07-02 00:00:00', 0, 0)
INSERT INTO [dbo].[Agenda] ([Id], [MedicoId], [PacienteId], [PacienteNome], [Convenio], [Telefone], [Horario], [Data], [Confirmada], [ConsultaCriada]) VALUES (9, 4, 1, NULL, NULL, NULL, N'2019-06-29 16:45:00', N'2019-07-02 00:00:00', 1, 1)
SET IDENTITY_INSERT [dbo].[Agenda] OFF


SET IDENTITY_INSERT [dbo].[Consultas] ON
INSERT INTO [dbo].[Consultas] ([Id], [Realizada], [MedicoId], [MedicoNome], [PacienteId], [PacienteNome], [Horario], [Data], [Peso], [Altura], [Anotacoes], [CIDId]) VALUES (1, 0, 4, NULL, 1, NULL, N'2019-06-29 15:20:00', N'2019-06-29 00:00:00', NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Consultas] ([Id], [Realizada], [MedicoId], [MedicoNome], [PacienteId], [PacienteNome], [Horario], [Data], [Peso], [Altura], [Anotacoes], [CIDId]) VALUES (2, 0, 4, NULL, 1, NULL, N'2019-06-29 14:20:00', N'2019-06-29 00:00:00', NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Consultas] ([Id], [Realizada], [MedicoId], [MedicoNome], [PacienteId], [PacienteNome], [Horario], [Data], [Peso], [Altura], [Anotacoes], [CIDId]) VALUES (3, 0, 4, NULL, 1, NULL, N'2019-06-29 10:20:00', N'2019-06-29 00:00:00', NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Consultas] ([Id], [Realizada], [MedicoId], [MedicoNome], [PacienteId], [PacienteNome], [Horario], [Data], [Peso], [Altura], [Anotacoes], [CIDId]) VALUES (4, 0, 4, NULL, 1, NULL, N'2019-06-29 10:20:00', N'2019-06-30 00:00:00', NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Consultas] ([Id], [Realizada], [MedicoId], [MedicoNome], [PacienteId], [PacienteNome], [Horario], [Data], [Peso], [Altura], [Anotacoes], [CIDId]) VALUES (5, 1, 4, NULL, 1, NULL, N'2019-06-29 10:20:00', N'2019-03-21 00:00:00', NULL, NULL, N'teste de anotações da consulta', NULL)
INSERT INTO [dbo].[Consultas] ([Id], [Realizada], [MedicoId], [MedicoNome], [PacienteId], [PacienteNome], [Horario], [Data], [Peso], [Altura], [Anotacoes], [CIDId]) VALUES (6, 1, 4, NULL, 1, NULL, N'2019-06-29 10:20:00', N'2019-06-28 00:00:00', NULL, NULL, N'Teste de anotações da consulta. /n Linha2.', NULL)
INSERT INTO [dbo].[Consultas] ([Id], [Realizada], [MedicoId], [MedicoNome], [PacienteId], [PacienteNome], [Horario], [Data], [Peso], [Altura], [Anotacoes], [CIDId]) VALUES (7, 1, 4, NULL, 1, NULL, N'2019-06-29 16:45:00', N'2019-07-02 00:00:00', NULL, NULL, N'anotações 1', NULL)
SET IDENTITY_INSERT [dbo].[Consultas] OFF


SET IDENTITY_INSERT [dbo].[ConsultaMedicamentoes] ON
INSERT INTO [dbo].[ConsultaMedicamentoes] ([Id], [ConsultaId], [MedicamentoId], [Posologia]) VALUES (1, 7, 4442, N'Tomar 1 comprimido ao dia após o almoço por 10 dias')
INSERT INTO [dbo].[ConsultaMedicamentoes] ([Id], [ConsultaId], [MedicamentoId], [Posologia]) VALUES (2, 7, 4443, N'Tomar 1 comprimido após o café da manhã por 5 dias')
SET IDENTITY_INSERT [dbo].[ConsultaMedicamentoes] OFF


SET IDENTITY_INSERT [dbo].[Exames] ON
insert into Exames (Id,Codigo,Descricao) values (1,N'1.1','Raio X torax')
insert into Exames (Id,Codigo,Descricao) values (2,N'1.2','Ultrassom da região abdominal')
insert into Exames (Id,Codigo,Descricao) values (3,N'1.3','Endoscopia')
insert into Exames (Id,Codigo,Descricao) values (4,N'1.4','Bronscopia')
SET IDENTITY_INSERT [dbo].[Exames] OFF

SET IDENTITY_INSERT [dbo].[ConsultaExames] ON
INSERT INTO [dbo].[ConsultaExames] ([Id], [ConsultaId], [ExameId], [Realizado], [DataLiberacaoExame], [Laudo], [Paciente], [Medico], [DataConsulta], [Discriminator]) VALUES (1, 7, 2, 0, NULL, NULL, NULL, NULL, NULL, N'ConsultaExame')
INSERT INTO [dbo].[ConsultaExames] ([Id], [ConsultaId], [ExameId], [Realizado], [DataLiberacaoExame], [Laudo], [Paciente], [Medico], [DataConsulta], [Discriminator]) VALUES (2, 7, 3, 0, NULL, NULL, NULL, NULL, NULL, N'ConsultaExame')
INSERT INTO [dbo].[ConsultaExames] ([Id], [ConsultaId], [ExameId], [Realizado], [DataLiberacaoExame], [Laudo], [Paciente], [Medico], [DataConsulta], [Discriminator]) VALUES (3, 7, 3, 1, N'2019-06-29 00:00:00', N'aaaaa', NULL, NULL, NULL, N'ConsultaExame')
SET IDENTITY_INSERT [dbo].[ConsultaExames] OFF









