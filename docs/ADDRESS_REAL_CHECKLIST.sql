SET NOCOUNT ON;

PRINT 'Resumo geral';

SELECT
    COUNT(*) AS TotalEntregas,
    SUM(CASE WHEN EnderecoId IS NULL THEN 1 ELSE 0 END) AS EntregasSemEnderecoId
FROM Entregas;

SELECT
    COUNT(*) AS TotalEnderecos,
    SUM(CASE WHEN Latitude IS NULL OR Longitude IS NULL THEN 1 ELSE 0 END) AS EnderecosSemCoordenadas
FROM Enderecos;

PRINT 'Entregas sem endereco vinculado';

SELECT
    Id,
    Codigo,
    Nome,
    ClienteNome,
    EnderecoId,
    MotoristaId,
    DestinatarioUsuarioId,
    Status
FROM Entregas
WHERE EnderecoId IS NULL
ORDER BY Codigo;

PRINT 'Entregas com endereco vinculado, mas sem coordenadas';

SELECT
    e.Id AS EntregaId,
    e.Codigo,
    e.Nome,
    e.ClienteNome,
    e.EnderecoId,
    d.Logradouro,
    d.Numero,
    d.Bairro,
    d.Cidade,
    d.Estado,
    d.Cep,
    d.Latitude,
    d.Longitude
FROM Entregas e
INNER JOIN Enderecos d ON d.Id = e.EnderecoId
WHERE d.Latitude IS NULL
   OR d.Longitude IS NULL
ORDER BY e.Codigo;

PRINT 'Amostra de payload esperado para o front';

SELECT TOP (20)
    e.Id,
    e.Codigo,
    e.Nome,
    e.ClienteNome,
    e.EnderecoId,
    d.Id AS EnderecoExpandidoId,
    d.Logradouro,
    d.Numero,
    d.Complemento,
    d.Bairro,
    d.Cidade,
    d.Estado,
    d.Cep,
    d.Referencia,
    d.Latitude,
    d.Longitude
FROM Entregas e
LEFT JOIN Enderecos d ON d.Id = e.EnderecoId
ORDER BY e.Codigo;
