DROP PROCEDURE IF EXISTS create_contest;
DROP TYPE IF EXISTS "problem_data";

-- CREATE TYPE problem_data AS (
--     name TEXT,
--     label TEXT,
--     order_num INT
-- );
--
-- DROP PROCEDURE IF EXISTS create_contest;
-- CREATE OR REPLACE PROCEDURE create_contest(
--     contest_name TEXT,
--     site_url TEXT,
--     start_date TIMESTAMP,
--     end_date TIMESTAMP,
--     local_id INT,
--     problems problem_data[]
-- )
--     LANGUAGE plpgsql
-- AS $$
-- DECLARE
--     new_contest_id INT;
--     p problem_data;
-- BEGIN
--     IF contest_name IS NULL OR contest_name = '' THEN
--         RAISE NOTICE 'Contest name is null or empty. Procedure aborted.';
--         RETURN;
--     END IF;
--
--     FOREACH p IN ARRAY problems LOOP
--             IF p.name IS NULL OR p.name = '' THEN
--                 RAISE NOTICE 'Problem name is null or empty. Procedure aborted.';
--                 RETURN;
--             END IF;
--         END LOOP;
--
--     INSERT INTO "Contests"("Name", "SiteUrl", "StartDate", "EndDate", "LocalId")
--     VALUES (contest_name, site_url, start_date, end_date, local_id)
--     RETURNING "Id" INTO new_contest_id;
--
--     FOREACH p IN ARRAY problems LOOP
--             INSERT INTO "Problems"("ContestId", "Name", "Label", "Order")
--             VALUES (new_contest_id, p.name, p.label, p.order_num);
--         END LOOP;
-- END;
-- $$;

-- CALL create_contest(
--     'Sample Contest',
--     'https://example.com',
--     '2025-07-05 13:00:00',
--     '2025-07-05 18:00:00',
--     1,
-- ARRAY[
--     ROW('Problem A', 'A', 1)::problem_data,
--     ROW('Problem B', 'B', 2)::problem_data
--     ]
-- );