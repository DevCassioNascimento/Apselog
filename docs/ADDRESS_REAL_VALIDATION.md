# Validacao de Endereco Real

## Objetivo

Confirmar que o front esta consumindo endereco real a partir de `GET /api/Entrega`, com `endereco` expandido no payload, e identificar dados faltantes no banco.

## Payload esperado em `/api/Entrega`

Cada item de entrega deve conter:

- `enderecoId`
- `endereco.id`
- `endereco.logradouro`
- `endereco.numero`
- `endereco.complemento`
- `endereco.bairro`
- `endereco.cidade`
- `endereco.estado`
- `endereco.cep`
- `endereco.referencia`
- `endereco.latitude`
- `endereco.longitude`

## Como validar via HTTP

Use o arquivo [Apselog.API.http](../src/Apselog.API/Apselog.API.http) para:

1. Fazer login
2. Guardar o `jwt_token`
3. Consultar `/api/Entrega`
4. Consultar `/api/Endereco`

## Como auditar dados faltantes

Execute o script [ADDRESS_REAL_CHECKLIST.sql](./ADDRESS_REAL_CHECKLIST.sql) na base `ApseLog`.

Ele cobre:

- Entregas sem `EnderecoId`
- Enderecos sem `Latitude` ou `Longitude`
- Entregas com endereco vinculado, mas incompleto para o mapa
- Amostra de join entre `Entregas` e `Enderecos`

## Observacao sobre este ambiente

Nesta maquina, a validacao contra a base nao foi concluida porque a instancia `MSSQLLocalDB` falhou ao iniciar com o erro:

- `Cannot create an automatic instance`

Enquanto isso nao for corrigido no ambiente Windows/LocalDB, o contrato esta garantido pelo codigo da API, mas a verificacao runtime depende da recuperacao da instancia local.
