CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Proveedores` (
    `Id` varchar(255) NOT NULL,
    `Nombre` longtext NULL,
    `Host` longtext NULL,
    `Usuario` longtext NULL,
    `Password` longtext NULL,
    CONSTRAINT `PK_Proveedores` PRIMARY KEY (`Id`)
);

CREATE TABLE `StampyAuthorizations` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `AccessToken` longtext NULL,
    `GotAt` longtext NULL,
    `ExpiresIn` int NOT NULL,
    `TokenType` longtext NULL,
    `RefreshToken` longtext NULL,
    CONSTRAINT `PK_StampyAuthorizations` PRIMARY KEY (`Id`)
);

CREATE TABLE `Templates` (
    `Id` varchar(255) NOT NULL,
    `Nombre` longtext NULL,
    `Html` longtext NULL,
    CONSTRAINT `PK_Templates` PRIMARY KEY (`Id`)
);

CREATE TABLE `UsuariosOnpremise` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Usuario` longtext NULL,
    `IdEmpresa` longtext NULL,
    CONSTRAINT `PK_UsuariosOnpremise` PRIMARY KEY (`Id`)
);

CREATE TABLE `CampanasGrupos` (
    `IdCampana` varchar(255) NOT NULL,
    `IdGrupo` varchar(255) NOT NULL,
    CONSTRAINT `PK_CampanasGrupos` PRIMARY KEY (`IdGrupo`, `IdCampana`)
);

CREATE TABLE `DuracionesCampanas` (
    `IdCampana` varchar(255) NOT NULL,
    `IdGrupo` varchar(255) NOT NULL,
    CONSTRAINT `PK_DuracionesCampanas` PRIMARY KEY (`IdGrupo`, `IdCampana`),
    CONSTRAINT `FK_DuracionesCampanas_CampanasGrupos_IdGrupo_IdCampana` FOREIGN KEY (`IdGrupo`, `IdCampana`) REFERENCES `CampanasGrupos` (`IdGrupo`, `IdCampana`) ON DELETE CASCADE
);

CREATE TABLE `Duraciones` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FechaDesde` datetime(6) NOT NULL,
    `FechaHasta` datetime(6) NOT NULL,
    `DuracionCampanaIdGrupo` varchar(255) NULL,
    `DuracionCampanaIdCampana` varchar(255) NULL,
    CONSTRAINT `PK_Duraciones` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Duraciones_DuracionesCampanas_DuracionCampanaIdGrupo_Duracio~` FOREIGN KEY (`DuracionCampanaIdGrupo`, `DuracionCampanaIdCampana`) REFERENCES `DuracionesCampanas` (`IdGrupo`, `IdCampana`) ON DELETE RESTRICT
);

CREATE TABLE `Recursos` (
    `Id` varchar(255) NOT NULL,
    `Hash` longtext NULL,
    `Nombre` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    `FirmaId` varchar(255) NULL,
    `CampanaId` varchar(255) NULL,
    `RssFeedId` varchar(255) NULL,
    CONSTRAINT `PK_Recursos` PRIMARY KEY (`Id`)
);

CREATE TABLE `ReglasEx` (
    `Id` varchar(255) NOT NULL,
    `Nombre` longtext NULL,
    `IdGrupo` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    `GrupoExId` varchar(255) NULL,
    `DuracionesIdGrupo` varchar(255) NULL,
    `DuracionesIdCampana` varchar(255) NULL,
    `Html` longtext NULL,
    CONSTRAINT `PK_ReglasEx` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ReglasEx_DuracionesCampanas_DuracionesIdGrupo_DuracionesIdCa~` FOREIGN KEY (`DuracionesIdGrupo`, `DuracionesIdCampana`) REFERENCES `DuracionesCampanas` (`IdGrupo`, `IdCampana`) ON DELETE RESTRICT
);

CREATE TABLE `CuentasGestionadas` (
    `Id` varchar(255) NOT NULL,
    `EmpresaId` varchar(255) NULL,
    `Nombre` longtext NULL,
    `EmailId` varchar(255) NULL,
    `FirmaId` varchar(255) NULL,
    CONSTRAINT `PK_CuentasGestionadas` PRIMARY KEY (`Id`)
);

CREATE TABLE `Campanas` (
    `Id` varchar(255) NOT NULL,
    `Nombre` longtext NULL,
    `LinkId` varchar(255) NULL,
    `BannerId` varchar(255) NULL,
    `Html` longtext NULL,
    `Color` longtext NULL,
    `FechaDesde` longtext NULL,
    `FechaHasta` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    CONSTRAINT `PK_Campanas` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Campanas_Recursos_BannerId` FOREIGN KEY (`BannerId`) REFERENCES `Recursos` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Dominios` (
    `Id` varchar(255) NOT NULL,
    `Nombre` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    `SmtpHost` longtext NULL,
    `SmtpPort` int NOT NULL,
    `RelayHost` longtext NULL,
    `RelayPort` int NOT NULL,
    CONSTRAINT `PK_Dominios` PRIMARY KEY (`Id`)
);

CREATE TABLE `Emails` (
    `Id` varchar(255) NOT NULL,
    `EmpresaId` varchar(255) NULL,
    `Nombre` longtext NULL,
    `Correo` longtext NULL,
    CONSTRAINT `PK_Emails` PRIMARY KEY (`Id`)
);

CREATE TABLE `EmpresasTemplates` (
    `IdEmpresa` varchar(255) NOT NULL,
    `IdTemplate` varchar(255) NOT NULL,
    CONSTRAINT `PK_EmpresasTemplates` PRIMARY KEY (`IdTemplate`, `IdEmpresa`),
    CONSTRAINT `FK_EmpresasTemplates_Templates_IdTemplate` FOREIGN KEY (`IdTemplate`) REFERENCES `Templates` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `EmpresasWatermarks` (
    `IdEmpresa` varchar(255) NOT NULL,
    `IdWatermark` varchar(255) NOT NULL,
    CONSTRAINT `PK_EmpresasWatermarks` PRIMARY KEY (`IdWatermark`, `IdEmpresa`)
);

CREATE TABLE `Firmas` (
    `Id` varchar(255) NOT NULL,
    `Nombre` longtext NULL,
    `EmpresaNombre` longtext NULL,
    `EmpresaDireccion` longtext NULL,
    `EmpresaLocalidad` longtext NULL,
    `EmpresaCodigoPostal` longtext NULL,
    `EmpresaProvincia` longtext NULL,
    `EmpresaPais` longtext NULL,
    `EmpresaNif` longtext NULL,
    `DepartamentoNombre` longtext NULL,
    `Titulo` longtext NULL,
    `Telefono` longtext NULL,
    `Movil` longtext NULL,
    `Email` longtext NULL,
    `UrlFacebook` longtext NULL,
    `UrlLinkedIn` longtext NULL,
    `UrlTwitter` longtext NULL,
    `UrlInstagram` longtext NULL,
    `UrlYouTube` longtext NULL,
    `LetraFuente` longtext NULL,
    `LetraTamano` double NOT NULL,
    `LetraColor` longtext NULL,
    `Rango` int NOT NULL,
    `LinkId` varchar(255) NULL,
    `Extra1` longtext NULL,
    `Extra2` longtext NULL,
    `Extra3` longtext NULL,
    `Extra4` longtext NULL,
    `TemplateId` varchar(255) NULL,
    `IdGrupo` varchar(255) NULL,
    `IdLayoutEmpresa` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    `LayoutId` varchar(255) NULL,
    `IdLayoutFirma` longtext NULL,
    `PixelId` varchar(255) NULL,
    `IdLayoutGrupo` longtext NULL,
    CONSTRAINT `PK_Firmas` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Firmas_Firmas_LayoutId` FOREIGN KEY (`LayoutId`) REFERENCES `Firmas` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Firmas_Recursos_PixelId` FOREIGN KEY (`PixelId`) REFERENCES `Recursos` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Firmas_Templates_TemplateId` FOREIGN KEY (`TemplateId`) REFERENCES `Templates` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Empresas` (
    `Id` varchar(255) NOT NULL,
    `IdLayout` varchar(255) NULL,
    `FlagRelay` int NOT NULL,
    `Nombre` longtext NULL,
    CONSTRAINT `PK_Empresas` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Empresas_Firmas_IdLayout` FOREIGN KEY (`IdLayout`) REFERENCES `Firmas` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Grupos` (
    `Id` varchar(255) NOT NULL,
    `EmpresaId` varchar(255) NULL,
    `Nombre` longtext NULL,
    `IdLayout` varchar(255) NULL,
    CONSTRAINT `PK_Grupos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Grupos_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Grupos_Firmas_IdLayout` FOREIGN KEY (`IdLayout`) REFERENCES `Firmas` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `GruposEx` (
    `Id` varchar(255) NOT NULL,
    `EmpresaId` varchar(255) NULL,
    `Nombre` longtext NULL,
    CONSTRAINT `PK_GruposEx` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_GruposEx_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Links` (
    `Id` varchar(255) NOT NULL,
    `Hash` longtext NULL,
    `Url` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    CONSTRAINT `PK_Links` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Links_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `RssFeeds` (
    `Id` varchar(255) NOT NULL,
    `EmpresaId` varchar(255) NULL,
    `Value` longtext NULL,
    `FirmaId` varchar(255) NULL,
    CONSTRAINT `PK_RssFeeds` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RssFeeds_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_RssFeeds_Firmas_FirmaId` FOREIGN KEY (`FirmaId`) REFERENCES `Firmas` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `UsuarioEx` (
    `Id` varchar(255) NOT NULL,
    `Email` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    CONSTRAINT `PK_UsuarioEx` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UsuarioEx_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Watermarks` (
    `Id` varchar(255) NOT NULL,
    `Mensaje` longtext NULL,
    `EmpresaId` varchar(255) NULL,
    CONSTRAINT `PK_Watermarks` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Watermarks_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Trackers` (
    `Id` varchar(255) NOT NULL,
    `EmpresaId` varchar(255) NULL,
    `Hash` longtext NULL,
    `RssFeedId` varchar(255) NULL,
    CONSTRAINT `PK_Trackers` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Trackers_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Trackers_RssFeeds_RssFeedId` FOREIGN KEY (`RssFeedId`) REFERENCES `RssFeeds` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `GruposExUsuariosEx` (
    `IdGrupo` varchar(255) NOT NULL,
    `IdUsuario` varchar(255) NOT NULL,
    CONSTRAINT `PK_GruposExUsuariosEx` PRIMARY KEY (`IdGrupo`, `IdUsuario`),
    CONSTRAINT `FK_GruposExUsuariosEx_GruposEx_IdGrupo` FOREIGN KEY (`IdGrupo`) REFERENCES `GruposEx` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_GruposExUsuariosEx_UsuarioEx_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `UsuarioEx` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Campanas_BannerId` ON `Campanas` (`BannerId`);

CREATE INDEX `IX_Campanas_EmpresaId` ON `Campanas` (`EmpresaId`);

CREATE INDEX `IX_Campanas_LinkId` ON `Campanas` (`LinkId`);

CREATE INDEX `IX_CampanasGrupos_IdCampana` ON `CampanasGrupos` (`IdCampana`);

CREATE INDEX `IX_CuentasGestionadas_EmailId` ON `CuentasGestionadas` (`EmailId`);

CREATE INDEX `IX_CuentasGestionadas_EmpresaId` ON `CuentasGestionadas` (`EmpresaId`);

CREATE INDEX `IX_CuentasGestionadas_FirmaId` ON `CuentasGestionadas` (`FirmaId`);

CREATE INDEX `IX_Dominios_EmpresaId` ON `Dominios` (`EmpresaId`);

CREATE INDEX `IX_Duraciones_DuracionCampanaIdGrupo_DuracionCampanaIdCampana` ON `Duraciones` (`DuracionCampanaIdGrupo`, `DuracionCampanaIdCampana`);

CREATE INDEX `IX_Emails_EmpresaId` ON `Emails` (`EmpresaId`);

CREATE INDEX `IX_Empresas_IdLayout` ON `Empresas` (`IdLayout`);

CREATE INDEX `IX_EmpresasTemplates_IdEmpresa` ON `EmpresasTemplates` (`IdEmpresa`);

CREATE INDEX `IX_EmpresasWatermarks_IdEmpresa` ON `EmpresasWatermarks` (`IdEmpresa`);

CREATE INDEX `IX_Firmas_EmpresaId` ON `Firmas` (`EmpresaId`);

CREATE INDEX `IX_Firmas_IdGrupo` ON `Firmas` (`IdGrupo`);

CREATE INDEX `IX_Firmas_LayoutId` ON `Firmas` (`LayoutId`);

CREATE INDEX `IX_Firmas_LinkId` ON `Firmas` (`LinkId`);

CREATE INDEX `IX_Firmas_PixelId` ON `Firmas` (`PixelId`);

CREATE INDEX `IX_Firmas_TemplateId` ON `Firmas` (`TemplateId`);

CREATE INDEX `IX_Grupos_EmpresaId` ON `Grupos` (`EmpresaId`);

CREATE INDEX `IX_Grupos_IdLayout` ON `Grupos` (`IdLayout`);

CREATE INDEX `IX_GruposEx_EmpresaId` ON `GruposEx` (`EmpresaId`);

CREATE INDEX `IX_GruposExUsuariosEx_IdUsuario` ON `GruposExUsuariosEx` (`IdUsuario`);

CREATE INDEX `IX_Links_EmpresaId` ON `Links` (`EmpresaId`);

CREATE INDEX `IX_Recursos_CampanaId` ON `Recursos` (`CampanaId`);

CREATE INDEX `IX_Recursos_EmpresaId` ON `Recursos` (`EmpresaId`);

CREATE INDEX `IX_Recursos_FirmaId` ON `Recursos` (`FirmaId`);

CREATE UNIQUE INDEX `IX_Recursos_RssFeedId` ON `Recursos` (`RssFeedId`);

CREATE INDEX `IX_ReglasEx_EmpresaId` ON `ReglasEx` (`EmpresaId`);

CREATE INDEX `IX_ReglasEx_GrupoExId` ON `ReglasEx` (`GrupoExId`);

CREATE INDEX `IX_ReglasEx_DuracionesIdGrupo_DuracionesIdCampana` ON `ReglasEx` (`DuracionesIdGrupo`, `DuracionesIdCampana`);

CREATE INDEX `IX_RssFeeds_EmpresaId` ON `RssFeeds` (`EmpresaId`);

CREATE UNIQUE INDEX `IX_RssFeeds_FirmaId` ON `RssFeeds` (`FirmaId`);

CREATE INDEX `IX_Trackers_EmpresaId` ON `Trackers` (`EmpresaId`);

CREATE UNIQUE INDEX `IX_Trackers_RssFeedId` ON `Trackers` (`RssFeedId`);

CREATE INDEX `IX_UsuarioEx_EmpresaId` ON `UsuarioEx` (`EmpresaId`);

CREATE INDEX `IX_Watermarks_EmpresaId` ON `Watermarks` (`EmpresaId`);

ALTER TABLE `CampanasGrupos` ADD CONSTRAINT `FK_CampanasGrupos_Campanas_IdCampana` FOREIGN KEY (`IdCampana`) REFERENCES `Campanas` (`Id`) ON DELETE CASCADE;

ALTER TABLE `CampanasGrupos` ADD CONSTRAINT `FK_CampanasGrupos_Grupos_IdGrupo` FOREIGN KEY (`IdGrupo`) REFERENCES `Grupos` (`Id`) ON DELETE CASCADE;

ALTER TABLE `Recursos` ADD CONSTRAINT `FK_Recursos_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Recursos` ADD CONSTRAINT `FK_Recursos_Campanas_CampanaId` FOREIGN KEY (`CampanaId`) REFERENCES `Campanas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Recursos` ADD CONSTRAINT `FK_Recursos_Firmas_FirmaId` FOREIGN KEY (`FirmaId`) REFERENCES `Firmas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Recursos` ADD CONSTRAINT `FK_Recursos_RssFeeds_RssFeedId` FOREIGN KEY (`RssFeedId`) REFERENCES `RssFeeds` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `ReglasEx` ADD CONSTRAINT `FK_ReglasEx_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `ReglasEx` ADD CONSTRAINT `FK_ReglasEx_GruposEx_GrupoExId` FOREIGN KEY (`GrupoExId`) REFERENCES `GruposEx` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `CuentasGestionadas` ADD CONSTRAINT `FK_CuentasGestionadas_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `CuentasGestionadas` ADD CONSTRAINT `FK_CuentasGestionadas_Emails_EmailId` FOREIGN KEY (`EmailId`) REFERENCES `Emails` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `CuentasGestionadas` ADD CONSTRAINT `FK_CuentasGestionadas_Firmas_FirmaId` FOREIGN KEY (`FirmaId`) REFERENCES `Firmas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Campanas` ADD CONSTRAINT `FK_Campanas_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Campanas` ADD CONSTRAINT `FK_Campanas_Links_LinkId` FOREIGN KEY (`LinkId`) REFERENCES `Links` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Dominios` ADD CONSTRAINT `FK_Dominios_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Emails` ADD CONSTRAINT `FK_Emails_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `EmpresasTemplates` ADD CONSTRAINT `FK_EmpresasTemplates_Empresas_IdEmpresa` FOREIGN KEY (`IdEmpresa`) REFERENCES `Empresas` (`Id`) ON DELETE CASCADE;

ALTER TABLE `EmpresasWatermarks` ADD CONSTRAINT `FK_EmpresasWatermarks_Empresas_IdEmpresa` FOREIGN KEY (`IdEmpresa`) REFERENCES `Empresas` (`Id`) ON DELETE CASCADE;

ALTER TABLE `EmpresasWatermarks` ADD CONSTRAINT `FK_EmpresasWatermarks_Watermarks_IdWatermark` FOREIGN KEY (`IdWatermark`) REFERENCES `Watermarks` (`Id`) ON DELETE CASCADE;

ALTER TABLE `Firmas` ADD CONSTRAINT `FK_Firmas_Empresas_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresas` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Firmas` ADD CONSTRAINT `FK_Firmas_Links_LinkId` FOREIGN KEY (`LinkId`) REFERENCES `Links` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Firmas` ADD CONSTRAINT `FK_Firmas_Grupos_IdGrupo` FOREIGN KEY (`IdGrupo`) REFERENCES `Grupos` (`Id`) ON DELETE RESTRICT;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200217122307_rss-feed', '2.2.6-servicing-10079');

ALTER TABLE `Proveedores` DROP COLUMN `Host`;

ALTER TABLE `Proveedores` DROP COLUMN `Password`;

ALTER TABLE `Proveedores` DROP COLUMN `Usuario`;

CREATE TABLE `ServersRelay` (
    `Id` varchar(255) NOT NULL,
    `Nombre` longtext NULL,
    `Host` longtext NULL,
    `Usuario` longtext NULL,
    `Password` longtext NULL,
    `ProveedorId` varchar(255) NULL,
    CONSTRAINT `PK_ServersRelay` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ServersRelay_Proveedores_ProveedorId` FOREIGN KEY (`ProveedorId`) REFERENCES `Proveedores` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_ServersRelay_ProveedorId` ON `ServersRelay` (`ProveedorId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200303091742_proveedores', '2.2.6-servicing-10079');

ALTER TABLE `ServersRelay` ADD `Port` int NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200304085443_smtp-port', '2.2.6-servicing-10079');

ALTER TABLE `Campanas` ADD `FechaCreacion` longtext NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200526085645_campana-fecha-creacion', '2.2.6-servicing-10079');

