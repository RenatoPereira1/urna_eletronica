$(document).ready(function() {
    grid();
});

function limpar() {
    formulario.idInput.value = '';
    formulario.cpfInput.value = '';
    formulario.nomeInput.value = '';
    $('#salvarBtn').html('Salvar');
}

function grid() {
    $.get('https://localhost:5001/Eleitor/Listar')
        .done(function(resposta) { 
            for(i = 0; i < resposta.length; i++) {                
                let linha = $('<tr class="text-center"></tr>');
                
                linha.append($('<td></td>').html(resposta[i].id));
                linha.append($('<td></td>').html(resposta[i].cpf));
                linha.append($('<td></td>').html(resposta[i].nome));
                
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
        url: 'https://localhost:5001/Eleitor/Excluir/',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(id),
        success: function(resposta) { 
            alert("Eleitor removido com sucesso!");
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
        endereco = 'https://localhost:5001/Eleitor/Alterar';
        mensagem = "Eleitor alterado com sucesso!";
        valorId = formulario.idInput.value;
    }
    else{
        metodo = 'POST';
        endereco = 'https://localhost:5001/Eleitor/Cadastrar';
        mensagem = "Eleitor cadastrado com sucesso!";
        valorId = 0;
    }

    let eleitor = {
        Id: valorId,
        Cpf: formulario.cpfInput.value,
        Nome: formulario.nomeInput.value
    };

    $.ajax({
        type: metodo,
        url: endereco,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(eleitor),
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
    $.get('https://localhost:5001/Eleitor/Visualizar?id='+id)
        .done(function(resposta) { 
            let visualizacao = resposta.id;
            visualizacao += '\n';
            visualizacao += resposta.nome;
            visualizacao += '\n';
            visualizacao += resposta.cpf;
            alert(visualizacao);
        })
        .fail(function(erro, mensagem, excecao) { 
            alert("Erro ao consultar a API!");
        });
}


function alterar(id){
    $.get('https://localhost:5001/Eleitor/Visualizar?id='+id)
        .done(function(resposta) { 
            $('#idInput').val(resposta.id);
            $('#nomeInput').val(resposta.nome);
            $('#cpfInput').val(resposta.cpf);
            $('#salvarBtn').html('Editar');
        })
        .fail(function(erro, mensagem, excecao) { 
            alert("Erro ao realizar a alteração!");
        });
}
