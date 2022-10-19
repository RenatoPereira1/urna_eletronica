$(document).ready(function() {
    grid();
});

function limpar() {
    formulario.idInput.value = '';
    formulario.numeroInput.value = '';
    formulario.nomeInput.value = '';
    formulario.partidoInput.value = '';
}

function grid() {
    $.get('https://localhost:5001/Candidato/Listar')
        .done(function(resposta) { 
            for(i = 0; i < resposta.length; i++) {                
                let linha = $('<tr class="text-center"></tr>');
                
                linha.append($('<td></td>').html(resposta[i].id));
                linha.append($('<td></td>').html(resposta[i].numero));
                linha.append($('<td></td>').html(resposta[i].nome));
                linha.append($('<td></td>').html(resposta[i].partido));
                
                let botaoExcluir = $('<button class="btn btn-danger"></button>').attr('type', 'button').html('Excluir').attr('onclick', 'excluir(' + resposta[i].id + ')');
                let botaoAlterar = $('<button class="btn btn-secondary"></button>').attr('type', 'button').html('Alterar').attr('onclick', 'alterar(' + resposta[i].id + ')');
                let botaoVisualizar = $('<button class="btn btn-secondary"></button>').attr('type', 'button').html('Ver').attr('onclick', 'visualizar(' + resposta[i].id + ')');

                let acoes = $('<td></td>');
                acoes.append(botaoVisualizar);
                acoes.append('&nbsp;')
                acoes.append(botaoAlterar);
                acoes.append('&nbsp;')
                acoes.append(botaoExcluir);

                linha.append(acoes);
                
                $('#grid').append(linha);
            }
        })
        .fail(function(erro, mensagem, excecao) { 
            alert("Erro ao consultar a API!");
        });
}

function excluir(id) {
    console.log(id)
    $.ajax({
        type: 'DELETE',
        url: 'https://localhost:5001/Candidato/Excluir/',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(id),
        success: function(resposta) { 
            if (resposta == true)
            {
                alert("Candidato removido com sucesso!");
            }
            else
            {
                alert("Erro no backend ao realizar a remoção do candidato!");
            }
            location.reload(true);
        },
        error: function(erro, mensagem, excecao) { 
            alert("Erro ao realizar a remoção!");
        }
    });
}

function cadastrar() {
    
    var endereco;
    var metodo;
    var mensagem;
    var valorId
    if ($('#salvarBtn').html() == 'Editar'){
        metodo = 'PUT';
        endereco = 'https://localhost:5001/Candidato/Alterar';
        mensagem = "Candidato alterado com sucesso!";
        valorId = formulario.idInput.value;
    }
    else{
        metodo = 'POST';
        endereco = 'https://localhost:5001/Candidato/Cadastrar';
        mensagem = "Candidato cadastrado com sucesso!";
        valorId = 0;
    }

    let candidato = {
        Id: valorId,
        Numero: formulario.numeroInput.value,
        Nome: formulario.nomeInput.value,
        Partido: formulario.partidoInput.value
    };

    $.ajax({
        type: metodo,
        url: endereco,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(candidato),
        success: function() {
            alert(mensagem);
            limpar();
            location.reload(true);
        },
        error: function() {
            alert("Erro ao realizar operação!");
        }
    });
}

function visualizar(id) {
    $.get('https://localhost:5001/Candidato/Visualizar?id='+id)
        .done(function(resposta) { 
            let visualizacao = resposta.id;
            visualizacao += '\n';
            visualizacao += resposta.numero;
            visualizacao += '\n';
            visualizacao += resposta.nome;
            visualizacao += '\n';
            visualizacao += resposta.partido;
            alert(visualizacao);
        })
        .fail(function(erro, mensagem, excecao) { 
            alert("Erro ao consultar a API!");
        });
}


function alterar(id){
    $.get('https://localhost:5001/Candidato/Visualizar?id='+id)
        .done(function(resposta) { 
            $('#idInput').val(resposta.id);
            $('#numeroInput').val(resposta.numero);
            $('#nomeInput').val(resposta.nome);
            $('#partidoInput').val(resposta.partido);
            $('#salvarBtn').html('Editar');
        })
        .fail(function(erro, mensagem, excecao) { 
            alert("Erro ao realizar a alteração!");
        });
}
