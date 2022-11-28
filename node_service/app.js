import { Boleto } from "broleto";
import express from "express";
import cepPromise from "cep-promise";
import fetch from "node-fetch";

const app = express();
const port = process.env.PORT || 3000;

app.use(express.json());
app.get("/readbankslip", (req, res) => {
    try {
        const barcode = req.query.barcode;

        if (barcode && barcode.length > 0) {
            let boleto = new Boleto(barcode);

            if (boleto.valid() === true) {
                let data = boleto.toJSON();
                data.barcode = barcode;

                return res.json({
                    data: data,
                    erro: false,
                    error: null,
                    message: "Boleto válido",
                });
            } else {
                return res
                    .status(400)
                    .json({
                        data: null,
                        erro: true,
                        error: null,
                        message: "Boleto inválido",
                    });
            }
        } else {
            return res
                .status(400)
                .json({
                    data: null,
                    erro: true,
                    error: null,
                    message: "Informe o código de barras do boleto",
                });
        }
    } catch (error) {
        return res
            .status(500)
            .json({
                data: null,
                erro: true,
                error: { message: error.message, stack: error.stack },
            });
    }
});

app.get("/seachZipCode/:zipCode", async (req, res) => {
    try {
        var zipCodeResponse = await cepPromise(req.params.zipCode);
        const result = {
            cep: zipCodeResponse.cep,
            logradouro: zipCodeResponse.street,
            bairro: zipCodeResponse.neighborhood,
            localidade: zipCodeResponse.city,
            uf: zipCodeResponse.state,
            service: zipCodeResponse.service,
        };

        res.status(200).json(result);
    } catch (error) {
        return res
            .status(200)
            .json({
                data: null,
                erro: true,
                error: { message: error.message, stack: error.stack },
            });
    }
});

app.post("/callapi", async (req, res) => {
    try {
        const { body } = req;
        let bodyData = {
            method: body.method,
            headers: body.headers
        }

        if(body.method === 'POST' || body.method === 'PUT' || body.method === 'PATCH')
            bodyData.body = JSON.stringify(body.data);

        const result = await fetch(body.url, bodyData);
        let data = null;
        try {
            data = await result.json();
        } catch (error) {/*ignored*/}

        return res.status(result.status).json(data);
    } catch (error) {
        return res
            .status(200)
            .json({
                data: null,
                erro: true,
                error: { message: error.message, stack: error.stack },
            });
    }
});

app.listen(port, () => {
    console.log(`App listening on port ${port}!`);
});
