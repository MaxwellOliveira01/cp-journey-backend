create table "__EFMigrationsHistory" (
    "MigrationId"    varchar(150) not null
        constraint "PK___EFMigrationsHistory"
            primary key,
    "ProductVersion" varchar(32)  not null
);

create table "Locals" (
    "Id"      integer generated by default as identity
        constraint "PK_Locals"
            primary key,
    "City"    text not null,
    "State"   text not null,
    "Country" text not null
);

create table "Contests" (
    "Id"            integer generated by default as identity
        constraint "PK_Contests"
            primary key,
    "Name"          varchar(200) not null,
    "SiteUrl"       varchar(250),
    "StartDate"     timestamp with time zone,
    "EndDate"       timestamp with time zone,
    "LocalId"       integer
        constraint "FK_Contests_Locals_LocalId"
            references "Locals",
    "StatementsPdf" bytea,
    "TutorialPdf"   bytea
);

create index "IX_Contests_LocalId"
    on "Contests" ("LocalId");

create table "Events" (
    "Id"          integer generated by default as identity
        constraint "PK_Events"
            primary key,
    "Name"        varchar(100) not null,
    "Description" varchar(500),
    "WebsiteUrl"  varchar(250),
    "Start"       timestamp with time zone,
    "End"         timestamp with time zone,
    "LocalId"     integer
        constraint "FK_Events_Locals_LocalId"
            references "Locals"
);

create index "IX_Events_LocalId"
    on "Events" ("LocalId");

create table "Universities"(
    "Id"      integer generated by default as identity
        constraint "PK_Universities"
            primary key,
    "Name"    varchar(200) not null,
    "Alias"   varchar(30)  not null,
    "LocalId" integer
        constraint "FK_Universities_Locals_LocalId"
            references "Locals"
);

create index "IX_Universities_LocalId"
    on "Universities" ("LocalId");

create table "Courses" (
    "Id"      integer generated by default as identity
        constraint "PK_Courses"
            primary key,
    "Name"    text not null,
    "Alias"   text not null
);

create table "Persons" (
    "Id"           integer generated by default as identity
        constraint "PK_Persons"
            primary key,
    "Name"         varchar(200) not null,
    "Handle"       varchar(200),
    "UniversityId" integer
        constraint "FK_Persons_Universities_UniversityId"
            references "Universities",
    "CoursesId" integer
        constraint "FK_Persons_Courses_CoursesId"
            references "Courses"
);

create index "IX_Persons_UniversityId"
    on "Persons" ("UniversityId");

create index "IX_Persons_CoursesId"
    on "Persons" ("CoursesId");

create table "Teams" (
    "Id"           integer generated by default as identity
        constraint "PK_Teams"
            primary key,
    "Name"         varchar(100) not null,
    "UniversityId" integer
        constraint "FK_Teams_Universities_UniversityId"
            references "Universities"
);

create index "IX_Teams_UniversityId"
    on "Teams" ("UniversityId");

create table "EventParticipations" (
    "EventId"  integer not null
        constraint "FK_EventParticipations_Events_EventId"
            references "Events"
            on delete cascade,
    "PersonId" integer not null
        constraint "FK_EventParticipations_Persons
        _PersonId"
            references "Persons"
            on delete cascade,
    constraint "PK_EventParticipations"
        primary key ("EventId", "PersonId")
);

create index "IX_EventParticipations_PersonId"
    on "EventParticipations" ("PersonId");

create table "Problems"(
    "Id"        integer generated by default as identity
        constraint "PK_Problems"
            primary key,
    "Name"      varchar(200) not null,
    "Label"     varchar(10)  not null,
    "Order"     integer      not null,
    "ContestId" integer      not null
        constraint "FK_Problems_Contests_ContestId"
            references "Contests"
            on delete cascade
);

create index "IX_Problems_ContestId"
    on "Problems" ("ContestId");

create table "TeamMembers" (
    "TeamId"   integer not null
        constraint "FK_TeamMembers_Teams_TeamId"
            references "Teams"
            on delete cascade,
    "PersonId" integer not null
        constraint "FK_TeamMembers_Persons_PersonId"
            references "Persons"
            on delete cascade,
    constraint "PK_TeamMembers"
        primary key ("PersonId", "TeamId")
);

create index "IX_TeamMembers_TeamId"
    on "TeamMembers" ("TeamId");

create table "TeamResults"(
    "Id"        integer generated by default as identity
        constraint "PK_TeamResults"
            primary key,
    "TeamId"    integer not null
        constraint "FK_TeamResults_Teams_TeamId"
            references "Teams"
            on delete cascade,
    "ContestId" integer not null
        constraint "FK_TeamResults_Contests_ContestId"
            references "Contests"
            on delete cascade,
    "Position"  integer not null,
    "Penalty"   integer not null
);

create index "IX_TeamResults_ContestId"
    on "TeamResults" ("ContestId");

create index "IX_TeamResults_TeamId"
    on "TeamResults" ("TeamId");

create table "Submissions" (
    "Id"           integer generated by default as identity
        constraint "PK_Submissions"
            primary key,
    "TeamResultId" integer not null
        constraint "FK_Submissions_TeamResults
        _TeamResultId"
            references "TeamResults"
            on delete cascade,
    "ProblemId"    integer not null
        constraint "FK_Submissions_Problems_ProblemId"
            references "Problems"
            on delete cascade,
    "Tries"        integer not null,
    "Accepted"     boolean not null,
    "Penalty"      integer not null
);

create index "IX_Submissions_ProblemId"
    on "Submissions" ("ProblemId");

create index "IX_Submissions_TeamResultId"
    on "Submissions" ("TeamResultId");

alter table "Locals" ADD CONSTRAINT UQ_Locals_City_State_Country 
    UNIQUE ("City", "State", "Country");


alter table "Courses" ADD CONSTRAINT UQ_Courses_Name
    UNIQUE ("Name");
