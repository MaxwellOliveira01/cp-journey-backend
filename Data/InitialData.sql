START TRANSACTION;

TRUNCATE TABLE "TeamResults" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "TeamMembers" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Problems" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Teams" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Contests" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Universities" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "EventParticipations" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Events" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Locals" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Persons" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "Submissions" RESTART IDENTITY CASCADE;

DROP PROCEDURE IF EXISTS create_contest;
DROP TYPE IF EXISTS "problem_data";

CREATE TYPE problem_data AS (
                                name TEXT,
                                label TEXT,
                                order_num INT
                            );

CREATE OR REPLACE PROCEDURE create_contest(
    contest_name TEXT,
    site_url TEXT,
    start_date TIMESTAMP,
    end_date TIMESTAMP,
    local_id INT,
    problems problem_data[]
)
    LANGUAGE plpgsql
AS $$
DECLARE
    new_contest_id INT;
    p problem_data;
BEGIN
    IF contest_name IS NULL OR contest_name = '' THEN
        RAISE NOTICE 'Contest name is null or empty. Procedure aborted.';
        RETURN;
    END IF;

    FOREACH p IN ARRAY problems LOOP
            IF p.name IS NULL OR p.name = '' THEN
                RAISE NOTICE 'Problem name is null or empty. Procedure aborted.';
                RETURN;
            END IF;
        END LOOP;

    INSERT INTO "Contests"("Name", "SiteUrl", "StartDate", "EndDate", "LocalId")
    VALUES (contest_name, site_url, start_date, end_date, local_id)
    RETURNING "Id" INTO new_contest_id;

    FOREACH p IN ARRAY problems LOOP
            INSERT INTO "Problems"("ContestId", "Name", "Label", "Order")
            VALUES (new_contest_id, p.name, p.label, p.order_num);
        END LOOP;
END;
$$;

INSERT INTO "Locals" ("Id", "City", "State", "Country") VALUES
                                                            ('1000', 'Brasilia', 'Federal District', 'Brazil'),
                                                            ('1001', 'Campinas', 'São Paulo', 'Brazil'),
                                                            ('1002', 'Uberlandia', 'Minas Gerais', 'Brazil'),
                                                            ('1003', 'Belo Horizonte', 'Minas Gerais', 'Brazil'),
                                                            ('1004', 'São Paulo', 'São Paulo', 'Brazil'),
                                                            ('1005', 'João Pessoa', 'Paraíba', 'Brazil');

SELECT * from "Locals";

INSERT INTO "Universities" ("Id", "Name", "Alias", "LocalId") VALUES
                                                                  ('1000', 'University of Brasília', 'UnB', '1000'),
                                                                  ('1001', 'University of Campinas', 'Unicamp', '1001'),
                                                                  ('1002', 'Federal University of Uberlândia', 'UFU', '1002'),
                                                                  ('1003', 'Federal University of Minas Gerais', 'UFMG', '1003'),
                                                                  ('1004', 'University of São Paulo', 'USP', '1004'),
                                                                  ('1005', 'Federal University of Paraíba', 'UFPB', '1005');

SELECT * from "Universities";


ROLLBACK;