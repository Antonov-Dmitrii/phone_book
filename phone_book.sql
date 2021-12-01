create database phone_book;
use phone_book;
create table department
(
id_department int primary key auto_increment,
id_parents int ,
name_department varchar(255)
);

create table users
(
id int primary key auto_increment not null,
first_name varchar(255),
last_name varchar(255),
phone int,
email varchar(255),
id_department int,
foreign key (id_department) references department(id_department)
);

insert into department (id_parents, name_department) values (0,'IT');
insert into department (id_parents, name_department) values (0,'Support');
insert into department (id_parents, name_department) values (1,'IT-Designer');
insert into department (id_parents, name_department) values (1,'IT-Developer');
insert into department (id_parents, name_department) values (4,'IT-Developer-Back_End');
insert into department (id_parents, name_department) values (4,'IT-Developer-Front_End');

insert into users (first_name, last_name, phone, email, id_department) values ('Antonov','Dmitry',292952481,'Antonov.dim2011@yandex.ru',5);
insert into users (first_name, last_name, phone, email, id_department) values ('Ivanov','Ivan',298134425,'Ivanov.Ivan@yandex.ru',6);
insert into users (first_name, last_name, phone, email, id_department) values ('Petrov','Petr',335362014,'Petrov_Petr@yandex.ru',5);
