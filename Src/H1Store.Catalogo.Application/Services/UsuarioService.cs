using AutoMapper;
using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using H1Store.Catalogo.Infra.Autenticacao;
using H1Store.Catalogo.Infra.Autenticacao.Models;
using H1Store.Catalogo.Infra.SenhaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.Services
{
	public class UsuarioService : IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly ICriptoService _senhaCriptografada;


		public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper,ITokenService tokenService, ICriptoService senhaCriptografada)
		{
			_usuarioRepository = usuarioRepository;
			_mapper = mapper;
			_tokenService = tokenService;
			_senhaCriptografada = senhaCriptografada;
		}

		public async Task<string> Autenticar(AutenticarUsuarioViewModel autenticarUsuarioViewModel )
		{
			string SenhaCifrada = _senhaCriptografada.SenhaCriptografada(autenticarUsuarioViewModel.Senha);


			var usuario = await _usuarioRepository
				.Autenticar(autenticarUsuarioViewModel.Login, SenhaCifrada);



			if (usuario == null)
				throw new ApplicationException("Login/Senha inválidos ou não existe");

			TokenRequest tokenRequest = new TokenRequest()
			{
			  usuario = autenticarUsuarioViewModel.Login
			};

			string token = await _tokenService.GerarTokenJWT(tokenRequest);

			return token;
		
		}

		public async Task Cadastrar(AutenticarUsuarioViewModel usuarioViewModel)
		{

			string SenhaCifrada = _senhaCriptografada.SenhaCriptografada(usuarioViewModel.Senha);
			usuarioViewModel.Senha = SenhaCifrada;

			var usuario = _mapper.Map<Usuario>(usuarioViewModel);
			await _usuarioRepository.Cadastrar(usuario);
		}
	}
}
