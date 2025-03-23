-- Query to set bit values
UPDATE DRANKJE
SET alcoholisch = CAST(CASE
WHEN 'Alcoholisch' = 'true' THEN 1
WHEN 'Non-alcoholisch' = 'true' THEN 0
ELSE NULL
END AS BIT)

