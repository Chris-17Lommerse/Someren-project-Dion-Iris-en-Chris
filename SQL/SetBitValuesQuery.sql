-- Query to set bit values
UPDATE SLAAPKAMER
SET type_kamer = CAST(CASE
WHEN 'student' = 'true' THEN 1
WHEN 'lecture' = 'true' THEN 0
ELSE NULL
END AS BIT)

-- Create a room
INSERT INTO SLAAPKAMER
VALUES('A1-01', 8, 0);