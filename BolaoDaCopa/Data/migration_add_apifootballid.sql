-- ==========================================================
-- Migração: adiciona coluna sportsdbid na tabela selecao
-- Executar no banco PostgreSQL (Neon) antes de subir a aplicação
-- API: TheSportsDB (https://www.thesportsdb.com)
-- League ID da Copa do Mundo 2026: 4429
-- IDs confirmados via: GET https://www.thesportsdb.com/api/v1/json/123/searchteams.php?t=<NomeTime>
-- ==========================================================

ALTER TABLE bolao.selecao
    ADD COLUMN IF NOT EXISTS sportsdbid INTEGER;

-- ==========================================================
-- Pre-população dos IDs do TheSportsDB por seleção (Copa do Mundo 2026)
-- Abreviações seguem o padrão FIFA.
-- Os UPDATEs que não encontrarem correspondência na tabela afetarão 0 linhas (sem erro).
-- NOTA: Para a Suíça, o código FIFA é 'SUI'. Caso o banco use 'CHE', ajuste abaixo.
-- ==========================================================

-- ── CONMEBOL ──────────────────────────────────────────────
UPDATE bolao.selecao SET sportsdbid = 134509 WHERE abreviacao = 'ARG'; -- Argentina
UPDATE bolao.selecao SET sportsdbid = 134499 WHERE abreviacao = 'CHI'; -- Chile
UPDATE bolao.selecao SET sportsdbid = 134501 WHERE abreviacao = 'COL'; -- Colombia
UPDATE bolao.selecao SET sportsdbid = 134507 WHERE abreviacao = 'ECU'; -- Ecuador
UPDATE bolao.selecao SET sportsdbid = 136471 WHERE abreviacao = 'PAR'; -- Paraguay
UPDATE bolao.selecao SET sportsdbid = 134504 WHERE abreviacao = 'URU'; -- Uruguay
UPDATE bolao.selecao SET sportsdbid = 136473 WHERE abreviacao = 'VEN'; -- Venezuela
UPDATE bolao.selecao SET sportsdbid = 134496 WHERE abreviacao = 'BRA'; -- Brazil

-- ── UEFA ──────────────────────────────────────────────────
UPDATE bolao.selecao SET sportsdbid = 135986 WHERE abreviacao = 'AUT'; -- Austria
UPDATE bolao.selecao SET sportsdbid = 133912 WHERE abreviacao = 'CRO'; -- Croatia
UPDATE bolao.selecao SET sportsdbid = 133906 WHERE abreviacao = 'DEN'; -- Denmark
UPDATE bolao.selecao SET sportsdbid = 133914 WHERE abreviacao = 'ENG'; -- England
UPDATE bolao.selecao SET sportsdbid = 133913 WHERE abreviacao = 'FRA'; -- France
UPDATE bolao.selecao SET sportsdbid = 133907 WHERE abreviacao = 'GER'; -- Germany
UPDATE bolao.selecao SET sportsdbid = 133902 WHERE abreviacao = 'GRE'; -- Greece
UPDATE bolao.selecao SET sportsdbid = 135987 WHERE abreviacao = 'HUN'; -- Hungary
UPDATE bolao.selecao SET sportsdbid = 133905 WHERE abreviacao = 'NED'; -- Netherlands
UPDATE bolao.selecao SET sportsdbid = 133901 WHERE abreviacao = 'POL'; -- Poland
UPDATE bolao.selecao SET sportsdbid = 133908 WHERE abreviacao = 'POR'; -- Portugal
UPDATE bolao.selecao SET sportsdbid = 136450 WHERE abreviacao = 'SCO'; -- Scotland
UPDATE bolao.selecao SET sportsdbid = 136140 WHERE abreviacao = 'SRB'; -- Serbia
UPDATE bolao.selecao SET sportsdbid = 136456 WHERE abreviacao = 'SVN'; -- Slovenia
UPDATE bolao.selecao SET sportsdbid = 133909 WHERE abreviacao = 'ESP'; -- Spain
UPDATE bolao.selecao SET sportsdbid = 134506 WHERE abreviacao = 'SUI'; -- Switzerland (FIFA: SUI)
UPDATE bolao.selecao SET sportsdbid = 134506 WHERE abreviacao = 'CHE'; -- Switzerland (ISO: CHE)
UPDATE bolao.selecao SET sportsdbid = 135985 WHERE abreviacao = 'TUR'; -- Turkey
UPDATE bolao.selecao SET sportsdbid = 133915 WHERE abreviacao = 'UKR'; -- Ukraine

-- ── CAF ───────────────────────────────────────────────────
UPDATE bolao.selecao SET sportsdbid = 134516 WHERE abreviacao = 'ALG'; -- Algeria
UPDATE bolao.selecao SET sportsdbid = 134498 WHERE abreviacao = 'CMR'; -- Cameroon
UPDATE bolao.selecao SET sportsdbid = 134502 WHERE abreviacao = 'CIV'; -- Côte d'Ivoire
UPDATE bolao.selecao SET sportsdbid = 136475 WHERE abreviacao = 'COD'; -- DR Congo
UPDATE bolao.selecao SET sportsdbid = 136138 WHERE abreviacao = 'EGY'; -- Egypt
UPDATE bolao.selecao SET sportsdbid = 134513 WHERE abreviacao = 'GHA'; -- Ghana
UPDATE bolao.selecao SET sportsdbid = 134580 WHERE abreviacao = 'MLI'; -- Mali
UPDATE bolao.selecao SET sportsdbid = 136139 WHERE abreviacao = 'MAR'; -- Morocco
UPDATE bolao.selecao SET sportsdbid = 134512 WHERE abreviacao = 'NGA'; -- Nigeria
UPDATE bolao.selecao SET sportsdbid = 136482 WHERE abreviacao = 'RSA'; -- South Africa
UPDATE bolao.selecao SET sportsdbid = 136143 WHERE abreviacao = 'SEN'; -- Senegal
UPDATE bolao.selecao SET sportsdbid = 136498 WHERE abreviacao = 'TAN'; -- Tanzania
UPDATE bolao.selecao SET sportsdbid = 136142 WHERE abreviacao = 'TUN'; -- Tunisia

-- ── AFC ───────────────────────────────────────────────────
UPDATE bolao.selecao SET sportsdbid = 134500 WHERE abreviacao = 'AUS'; -- Australia
UPDATE bolao.selecao SET sportsdbid = 134579 WHERE abreviacao = 'CHN'; -- China PR
UPDATE bolao.selecao SET sportsdbid = 134511 WHERE abreviacao = 'IRN'; -- Iran
UPDATE bolao.selecao SET sportsdbid = 140148 WHERE abreviacao = 'IRQ'; -- Iraq
UPDATE bolao.selecao SET sportsdbid = 140145 WHERE abreviacao = 'JOR'; -- Jordan
UPDATE bolao.selecao SET sportsdbid = 134503 WHERE abreviacao = 'JPN'; -- Japan
UPDATE bolao.selecao SET sportsdbid = 134517 WHERE abreviacao = 'KOR'; -- South Korea
UPDATE bolao.selecao SET sportsdbid = 136137 WHERE abreviacao = 'SAU'; -- Saudi Arabia
UPDATE bolao.selecao SET sportsdbid = 140151 WHERE abreviacao = 'UZB'; -- Uzbekistan

-- ── CONCACAF ──────────────────────────────────────────────
UPDATE bolao.selecao SET sportsdbid = 140073 WHERE abreviacao = 'CAN'; -- Canada
UPDATE bolao.selecao SET sportsdbid = 134505 WHERE abreviacao = 'CRC'; -- Costa Rica
UPDATE bolao.selecao SET sportsdbid = 140174 WHERE abreviacao = 'SLV'; -- El Salvador
UPDATE bolao.selecao SET sportsdbid = 134508 WHERE abreviacao = 'HON'; -- Honduras
UPDATE bolao.selecao SET sportsdbid = 140037 WHERE abreviacao = 'JAM'; -- Jamaica
UPDATE bolao.selecao SET sportsdbid = 134497 WHERE abreviacao = 'MEX'; -- Mexico
UPDATE bolao.selecao SET sportsdbid = 136141 WHERE abreviacao = 'PAN'; -- Panama
UPDATE bolao.selecao SET sportsdbid = 134514 WHERE abreviacao = 'USA'; -- USA

-- ── OFC ───────────────────────────────────────────────────
UPDATE bolao.selecao SET sportsdbid = 137449 WHERE abreviacao = 'NZL'; -- New Zealand

-- ==========================================================
-- Correções e seleções ausentes na migração original
-- (identificadas após executar o script e consultar o banco)
-- ==========================================================

-- Arábia Saudita: no banco a abreviação é 'KSA', não 'SAU'
UPDATE bolao.selecao SET sportsdbid = 136137 WHERE abreviacao = 'KSA'; -- Saudi Arabia

-- Seleções que estavam no banco com sportsdbid = NULL
UPDATE bolao.selecao SET sportsdbid = 134515 WHERE abreviacao = 'BEL'; -- Belgium
UPDATE bolao.selecao SET sportsdbid = 136477 WHERE abreviacao = 'CPV'; -- Cape Verde
UPDATE bolao.selecao SET sportsdbid = 140271 WHERE abreviacao = 'CUW'; -- Curaçao
UPDATE bolao.selecao SET sportsdbid = 133904 WHERE abreviacao = 'CZE'; -- Czech Republic
UPDATE bolao.selecao SET sportsdbid = 140175 WHERE abreviacao = 'HAI'; -- Haiti
UPDATE bolao.selecao SET sportsdbid = 136516 WHERE abreviacao = 'NOR'; -- Norway
UPDATE bolao.selecao SET sportsdbid = 136472 WHERE abreviacao = 'QAT'; -- Qatar
UPDATE bolao.selecao SET sportsdbid = 133916 WHERE abreviacao = 'SWE'; -- Sweden

UPDATE bolao.selecao SET sportsdbid = 134510 WHERE abreviacao = 'BIH'; -- Bosnia Herzegovina
