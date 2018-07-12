
$(document).ready(function () {

    $(".menu-esquerda-escondido").hover(
        function () {

            //$(".imagens-menu").animate({ height: 100 }, 400);
            //$(".navbar").animate({ height: 150 }, 400);
            //$(".navbar").css({ "border-bottom": "16px solid #74361e" });
            //$(".navbar , .imagens-menu").clearQueue();
            $(".texto-escondido").fadeIn("fast");
            $(".menu-esquerda-escondido").addClass("col-2", 30000).removeClass("col-1", 30000).fadeIn("slow");;

            $(".menu-esquerda-escondido , .texto-escondido").clearQueue();
        },
        function () {
            $(".texto-escondido").fadeOut("fast");
            $(".menu-esquerda-escondido").addClass("col-1", 3000).removeClass("col-2", 3000);

            $(".menu-esquerda-escondido , .texto-escondido").clearQueue();
            //$(".imagens-menu").animate({ height: 50 }, 400);
            //$(".navbar").animate({ height: 100 }, 400);
            //$(".navbar").css({ "border-bottom": "5px solid #74361e" });
            //$(".navbar , .imagens-menu").clearQueue();
        }
    );

    $('#valor').on('keyup', function () {

        var v = $("#valor").val();
        v = v.replace(/\D/g, "");
        v = new String(Number(v));
        var len = v.length;
        if (1 == len)
            v = v.replace(/(\d)/, "0,0$1");
        else if (2 == len)
            v = v.replace(/(\d)/, "0,$1");
        else if (len > 2) {
            v = v.replace(/(\d{2})$/, ',$1');
        }
        $("#valor").val(v);
    });

    $('#CNPJ').mask('99.999.999/9999-99');
    $('#Telefone').mask("(99) 9999-99999");
    $('#Telefone').change(function (event) { // mascara dinamica
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target); element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-9999");
        }
        else {
            element.mask("(99) 9999-99999");
        }
    });

    $('#Telefone2').mask("(99) 9999-99999");
    $('#Telefone2').change(function (event) { // mascara dinamica
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target); element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-9999");
        }
        else {
            element.mask("(99) 9999-99999");
        }
    });


    $("#cep").mask("99.999-999");
});


// busca endereco automaticamente
function retira_acentos(palavra) {
    com_acento = 'áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇ';
    sem_acento = 'aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC';
    nova = '';
    for (i = 0; i < palavra.length; i++) {
        if (com_acento.search(palavra.substr(i, 1)) >= 0) {
            nova += sem_acento.substr(com_acento.search(palavra.substr(i, 1)), 1);
        }
        else {
            nova += palavra.substr(i, 1);
        }
    }
    return nova.toUpperCase();
}

//function getEndereco() {
//    if ($.trim($("#CEP").val()) != "") {

//        var $j = jQuery.noConflict();
//        var cep_code = $("#CEP").val();
//        alert(cep_code);
//        if (cep_code.length <= 0) return;
//        $j.get("http://apps.widenet.com.br/busca-cep/api/cep.json", { code: cep_code },
//            function (result) {
//                if (result.status != 1) {
//                    alert(result.message || "Houve um erro desconhecido");
//                    return;
//                }
//                $("#Cidade").val(retira_acentos(unescape(result.city)));
//                $("#Bairro").val(unescape(result.district));
//                $("#Rua").val(unescape(result.address));
//                $("#Estado").val(unescape(result.state));
//            });
//    }
//}


function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('rua').value = ("");
    document.getElementById('bairro').value = ("");
    document.getElementById('cidade').value = ("");
    document.getElementById('uf').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('rua').value = (conteudo.logradouro);
        document.getElementById('bairro').value = (conteudo.bairro);
        document.getElementById('cidade').value = (conteudo.localidade);
        document.getElementById('uf').value = (conteudo.uf);
    } //end if.
    else {
        //CEP não Encontrado.
        limpa_formulário_cep();
        alert("CEP não encontrado.");
    }
}

function pesquisacep(valor) {

    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            document.getElementById('rua').value = "...";
            document.getElementById('bairro').value = "...";
            document.getElementById('cidade').value = "...";
            document.getElementById('uf').value = "...";

            //Cria um elemento javascript.
            var script = document.createElement('script');

            //Sincroniza com o callback.
            script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

            //Insere script no documento e carrega o conteúdo.
            document.body.appendChild(script);

        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();
            alert("Formato de CEP inválido.");
        }
    } //end if.
    else {
        //cep sem valor, limpa formulário.
        limpa_formulário_cep();
    }
};