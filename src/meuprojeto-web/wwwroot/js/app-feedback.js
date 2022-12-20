// Classe para padronização na forma de apresentar feebacks para o usuário

const TipoFeedback = {
    Atencao: "ATENCAO",
    Erro: "ERRO",
    Sucesso: "SUCESSO",
    Info: "INFO"
}

class Feedback {

    constructor(tipo = TipoFeedback.Info, mensagem = "Sua mensagem aqui.", mensagemAdicional = null, tipoAcao = 5, id = null) {
        this.tipo = tipo;
        this.mensagem = mensagem;
        this.mensagemAdicional = mensagemAdicional;
        this.tipoAcao = tipoAcao;
        this.id = id;
    }

    static converter(feedbackJson) {
        if (feedbackJson == null || !feedbackJson.hasOwnProperty("tipoDescricao") || !feedbackJson.hasOwnProperty("mensagem"))
            return null;

        return new Feedback(feedbackJson.tipoDescricao, feedbackJson.mensagem, feedbackJson.hasOwnProperty("mensagemAdicional") ? feedbackJson.mensagemAdicional : null, feedbackJson.hasOwnProperty("tipoAcao") ? feedbackJson.tipoAcao : null, feedbackJson.hasOwnProperty("id") ? feedbackJson.id : null);
    }

    exibir(fecharCallback) {
        let swalType = TipoFeedback.Info;

        switch (this.tipo.toUpperCase()) {
            case TipoFeedback.Atencao: swalType = "warning"; break;
            case TipoFeedback.Erro: swalType = "error"; break;
            case TipoFeedback.Sucesso: swalType = "success"; break;
        }

        let tipoAcao = this.tipoAcao;

        swal({
            title: this.mensagem,
            html: this.mensagemAdicional,
            type: swalType,
            confirmButtonColor: "#337AB7",
            showCancelButton: false,
            allowOutsideClick: false
        }).then(function () {
            if (fecharCallback != null) {
                fecharCallback();
                swal.close();
            } else {
                if (tipoAcao != null) {
                    switch (tipoAcao) {
                        case 0: swal.close(); break;
                        case 1: window.history.back(); break;
                        case 2: window.close(); break;
                        case 3: location.href = "/fornecedor"; break;
                        case 4: location.reload(); break;
                        case 5: AppModal.ocultar(); break;
                        case 6: location.href = _urlLoginPortal; break;
                    }
                }
            }
        });
    }
}

function exibir(fecharCallback) {

    let swalType = "INFO";
    let tipoAcao = this.tipoAcao;

    swal({
        title: "Titulo teste",
        text: "Mensagem adicional",
        type: "success",
        allowOutsideClick: false,
        allowEscapeKey: false
    }).then(function () {
        if (fecharCallback != null) {
            fecharCallback();
            swal.close();
        } else {
            if (tipoAcao != null) {
                switch (tipoAcao) {
                    case 0: swal.close(); break;
                    case 1: window.history.back(); break;
                    case 2: window.close(); break;
                    case 3: location.href = "/fornecedor"; break;
                    case 4: location.reload(); break;
                    case 5: AppModal.ocultar(); break;
                    case 6: location.href = _urlLoginPortal; break;
                }
            }
        }
    });
}