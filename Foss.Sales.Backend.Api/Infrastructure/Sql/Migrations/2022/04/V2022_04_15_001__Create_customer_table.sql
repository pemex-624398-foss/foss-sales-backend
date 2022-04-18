create table customer (
    id serial primary key,
    first_name varchar(50) not null,
    last_name varchar(50),
    email varchar(50),
    gender varchar(50),
    city varchar(50) not null
);
