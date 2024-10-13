

function stampaTabella(){
    $.ajax(
        {
            url: "http://localhost:5290/api/clienti",
            type: "GET",
            success: function(risultato){

                let contenuto = "";
                for(let [idx, item] of risultato.entries()){
                    contenuto += `
                        <tr>
                            <td>${item.nom}</td>
                            <td>${item.cog}</td>
                            <td>${item.ind}</td>
                            <td>${item.tel}</td>
                            <td>${item.ema}</td>
                            <td>
                                <button type="button" class="btn btn-warning" onclick="modifica('${item.cod}')">Modifica</button>
                            </td>
                            <td>
                                <button type="button" class="btn btn-danger" onclick="elimina('${item.cod}')">Elimina</button>
                            </td>
                        </tr>
                    `;
                }

                $("#tabella-clienti").html(contenuto);

            },
            error: function(errore){
                alert("Sto in errore");
                console.log(errore)
            }
        }
    );
}

function salva(){

    let nom = $("#ins-nome").val();
    let cog = $("#ins-cognome").val();
    let ind = $("#ins-indi").val();
    let tel = $("#ins-tele").val();
    let ema = $("#ins-ema").val();
    if(nom.trim() == ""){
        alert("Attenzione, il campo nome non può essere vuoto");
        $("input-nome").focus();
        return;
    }
    if(cog.trim() == ""){
        alert("Attenzione, il campo cognome non può essere vuoto");
        $("input-cogn").focus();
        return;
    }
    let cli = {
        nom,
        cog,
        ind,
        tel,
        ema
    };

    $.ajax(
        {
            url: "http://localhost:5290/api/clienti",
            type: "POST",
            data: JSON.stringify(cli),
            contentType: "application/json",
            success: function(){
                alert("Inserimento effettuato con successo");
                stampaTabella();
            },
            error: function(errore){
                alert("Errore di inserimento");
                console.log(errore)
            }
        }

    )
}
stampaTabella();


