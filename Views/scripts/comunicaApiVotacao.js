$(document).ready(function() {
    listarGrid();
    listarCandidato();
    listarCandidatoss();
    $('#salvarBtn').prop('disabled', true);
    $('#candidatoSelect').prop('disabled', true);
});



function listarCandidato(){
    $.get('https://localhost:5001/Candidato/Listar')
        .done(function(resposta) { 
            for(i = 0; i < resposta.length; i++) {
                $('#candidatoSelect').append($('<option></option>').val(resposta[i].id).html(resposta[i].nome));
            }
        })
        .fail(function(erro, mensagem, excecao) { 
            alert(mensagem + ': ' + excecao);
        });
}

function listarCandidatoss(){
    $.get('https://localhost:5001/Candidato/Listar')
        .done(function(resposta) { 
            for(i = 0; i < resposta.length; i++) {
                $('#candSelect').append($('<option></option>').val(resposta[i].id).html(resposta[i].nome));
            }
        })
        .fail(function(erro, mensagem, excecao) { 
            alert(mensagem + ': ' + excecao);
        });
}

function consultaEleitorPorCpf(cpf){
    $.get('https://localhost:5001/Eleitor/BuscaPorCpf?cpf='+cpf)
        .done(function(resposta) { 
            if (resposta == null)
            {
                $('#eleitorNome').html("Eleitor(a) não encontrado");
                $('#candidatoSelect').prop('disabled', true);            }
            else
            {
                $('#eleitorNome').html("Eleitor(a): " + resposta.nome);
                $('#eleitorId').val(resposta.id);
                $('#eleitorInput').prop('disabled', true);
                $('#candidatoSelect').prop('disabled', false);
            }
        })
        .fail(function(erro, mensagem, excecao) { 
            alert(mensagem + ': ' + excecao);
        });
}

function validaCandidato(){

    candidatoSelecionado = $('#candidatoSelect').val();

    if(candidatoSelecionado != "0")
    {
        $('#salvarBtn').prop('disabled', false);
    }
    else
    {
        $('#salvarBtn').prop('disabled', true);
    }
}

function limpar() {
    formulario.eleitorInput.value = '';
    formulario.candidatoSelect.value = 0;
    $('#eleitorNome').html("");
    $('#eleitorInput').prop('disabled', false);
    $('#salvarBtn').prop('disabled', true);
    $('#candidatoSelect').prop('disabled', true);
    $('#candidatoSelect').val("0");

}


function carregarGrid(resposta){
    $('#grid tr').remove();
    for(i = 0; i < resposta.length; i++) {                
        let linha = $('<tr class="text-center"></tr>');
        
        linha.append($('<td></td>').html(resposta[i].idEleitorNavigation.nome));
        linha.append($('<td></td>').html(resposta[i].idCandidatoNavigation.nome))
        
        $('#grid').append(linha);
    }
    let linha2 = $('<tr class="text-center"></tr>');
        
    linha2.append($('<td></td>').html("Total"));
    linha2.append($('<td></td>').html($("#table_id tr").length -1));
    $('#grid').append(linha2);

}

function listarGrid(){
    $.get('https://localhost:5001/Votacao/Listar')
        .done(function(resposta) { 
            carregarGrid(resposta);
        })
        .fail(function(erro, mensagem, excecao) { 
            alert(mensagem + ': ' + excecao);
        });
}




function listaVotosPorCandidato() {

    var element = document.getElementById("candSelect");
    var valueVotacao = element.options[element.selectedIndex].value;
    var textVotacao = element.options[element.selectedIndex].text;
    
    if(valueVotacao == 0){
        listarGrid();
    }
    else
    {
        $.get('https://localhost:5001/Votacao/ListarPorCandidato?id=' + valueVotacao)
            .done(function(resposta) { 
                carregarGrid(resposta);
            })
            .fail(function(erro, mensagem, excecao) { 
                alert("Erro ao consultar a API!");
            });
        }
}


function cadastrar() {
    
    let votacao = {
        Id: 0,
        IdCandidato: $('#candidatoSelect').val(),
        IdEleitor: $('#eleitorId').val()
    };

    console.log(votacao);

    $.ajax({
        type: 'POST',
        url: 'https://localhost:5001/Votacao/Cadastrar',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(votacao),
        success: function() {
            alert("Votação registrada com sucesso!");
            limpar();
            location.reload(true);
        },
        error: function() {
            alert("Erro ao realizar operação!");
        }
    });
}


