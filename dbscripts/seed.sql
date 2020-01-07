
\c blogdb

CREATE TABLE IF NOT  EXISTS blog
(
    id serial PRIMARY KEY,
    title  VARCHAR (50)  NOT NULL,
    description  VARCHAR (100)  NOT NULL
);

ALTER TABLE "blog" OWNER TO bloguser;

Insert into blog(title,description) values( 'Title 1','Description 1');
Insert into blog(title,description) values( 'Title 2','Description 2');
Insert into blog(title,description) values( 'Title 3','Description 3');
Insert into blog(title,description) values( 'Title 4','Description 4');
Insert into blog(title,description) values( 'Title 5','Description 5');
Insert into blog(title,description) values( 'Title 6','Description 6');
Insert into blog(title,description) values( 'Title 7','Description 7');
Insert into blog(title,description) values( 'Title 8','Description 8');
Insert into blog(title,description) values( 'Title 99','Description 99');
