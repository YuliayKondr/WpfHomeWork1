/*Database first*/
/*
CREATE TABLE employees
(
    id int NOT NULL,
    uid varchar(100) not null default '',
    password varchar(100) not null default '',
    name varchar(100) not null default '',
    last_name varchar(100) not null default '',
    username varchar(100) not null default '',
    email varchar(100) not null default '',
    avatar varchar(300) not null default '',
    gender varchar(20) not null default '',
    phone_number varchar(40) not null default '',
    social_insurance_number varchar(50) not null default '',
    birth_date datetime not null default '',
    credit_card TEXT not null default '',
    employment TEXT not null default '',
    id_address int not null default '' ,
    id_subscription int not null default '',
    PRIMARY KEY (id),
    FOREIGN KEY (id_address) REFERENCES address(id),
    FOREIGN KEY (id_subscription) REFERENCES subscription(id)
);

CREATE TABLE address
(
    id int NOT NULL AUTO_INCREMENT,
    city varchar(100) not null default '',
    street_name varchar(100) not null default '',
    street_address varchar(200) not null default '',
    zip_code varchar(20) not null default '',
    state varchar(50) not null default '',
    country varchar(50) not null default '',
    coordinates TEXT not null default '',
    PRIMARY KEY (id)
);

CREATE TABLE subscription
(
    id int NOT NULL AUTO_INCREMENT,
    plan VARCHAR(50) not null default '',
    status varchar(50) not null default '',
    payment_method VARCHAR(50) not null default '',
    term VARCHAR(50) not null default '',
    PRIMARY KEY (id)
);*/