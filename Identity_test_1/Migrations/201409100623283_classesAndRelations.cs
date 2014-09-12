namespace Identity_test_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classesAndRelations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.kategoris",
                c => new
                    {
                        kategoriId = c.Int(nullable: false, identity: true),
                        navn = c.String(),
                    })
                .PrimaryKey(t => t.kategoriId);
            
            CreateTable(
                "dbo.Oppslags",
                c => new
                    {
                        oppslagId = c.Int(nullable: false, identity: true),
                        tittel = c.String(),
                        ingress = c.String(),
                        oppslagTekst = c.String(),
                        dato = c.DateTime(),
                        treff = c.Int(),
                        imageUrl = c.String(),
                        videoUrl = c.String(),
                        eier_Id = c.String(maxLength: 128),
                        kategori_kategoriId = c.Int(),
                    })
                .PrimaryKey(t => t.oppslagId)
                .ForeignKey("dbo.AspNetUsers", t => t.eier_Id)
                .ForeignKey("dbo.kategoris", t => t.kategori_kategoriId)
                .Index(t => t.eier_Id)
                .Index(t => t.kategori_kategoriId);
            
            CreateTable(
                "dbo.kommentars",
                c => new
                    {
                        kommentarId = c.Int(nullable: false, identity: true),
                        tekst = c.String(),
                        dato = c.DateTime(nullable: false),
                        eier_Id = c.String(maxLength: 128),
                        oppslag_oppslagId = c.Int(),
                    })
                .PrimaryKey(t => t.kommentarId)
                .ForeignKey("dbo.AspNetUsers", t => t.eier_Id)
                .ForeignKey("dbo.Oppslags", t => t.oppslag_oppslagId)
                .Index(t => t.eier_Id)
                .Index(t => t.oppslag_oppslagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.kommentars", "oppslag_oppslagId", "dbo.Oppslags");
            DropForeignKey("dbo.kommentars", "eier_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Oppslags", "kategori_kategoriId", "dbo.kategoris");
            DropForeignKey("dbo.Oppslags", "eier_Id", "dbo.AspNetUsers");
            DropIndex("dbo.kommentars", new[] { "oppslag_oppslagId" });
            DropIndex("dbo.kommentars", new[] { "eier_Id" });
            DropIndex("dbo.Oppslags", new[] { "kategori_kategoriId" });
            DropIndex("dbo.Oppslags", new[] { "eier_Id" });
            DropTable("dbo.kommentars");
            DropTable("dbo.Oppslags");
            DropTable("dbo.kategoris");
        }
    }
}
