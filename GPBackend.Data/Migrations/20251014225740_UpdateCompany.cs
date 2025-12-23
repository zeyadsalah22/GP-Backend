using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "logo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "company_size",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "company_id", "careers_link", "company_size", "created_at", "description", "industry_id", "is_deleted", "linkedin_link", "location", "logo", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, "https://aelmasry.com/", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Personal portfolio of a Software Engineer.", 1, false, "https://www.linkedin.com/company/aelmasry/", "Cairo", "https://logo.clearbit.com/aelmasry.com", "Abdelaziz Elmasry", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "https://www.advansys-esc.com/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading technology solutions provider that helps businesses to digitally transform and empower their future.", 1, false, "https://www.linkedin.com/company/advansys-esc/", "Cairo", "https://logo.clearbit.com/advansys-esc.com", "Advansys", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "https://akam.com.eg/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A real estate development company that offers an unconventional vision of living in integrated communities with a unique quality of life.", 4, false, "https://www.linkedin.com/company/akam-developments/", "Cairo", "https://logo.clearbit.com/akam.com.eg", "Akam Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "https://www.alfuttaim.com/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A diversified conglomerate operating in automotive, financial services, real estate, and retail sectors.", 7, false, "https://www.linkedin.com/company/alfuttaim/", "Cairo", "https://logo.clearbit.com/alfuttaim.com", "Al-Futtaim", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "http://amoceg.com", "1001-5000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An Egyptian company specializing in the production of essential mineral oils, paraffin wax, and other petroleum products.", 6, false, "https://www.linkedin.com/company/amoceg/", "Alexandria", "https://logo.clearbit.com/amoceg.com", "Alexandria Mineral Oils Co. (AMOC)", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "https://alexforprog.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software house specializing in creating custom software solutions, including mobile and web applications.", 1, false, "https://www.linkedin.com/company/alexaiapps/", "Alexandria", "https://logo.clearbit.com/alexforprog.com", "Alexapps", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "https://www.algebraventures.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading Egyptian venture capital firm that invests in technology and technology-enabled startups.", 2, false, "https://www.linkedin.com/company/algebraventures/", "Cairo", "https://logo.clearbit.com/algebraventures.com", "Algebra Ventures", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "https://algoriza.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company that builds world-class engineering teams to help businesses scale.", 1, false, "https://www.linkedin.com/company/algoriza/", "Cairo", "https://logo.clearbit.com/algoriza.com", "Algoriza", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "https://www.almentor.net/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An online video e-learning platform in Arabic, offering courses and talks from mentors across various fields.", 10, false, "https://www.linkedin.com/company/almentor/", "Cairo", "https://logo.clearbit.com/almentor.net", "almentor", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "https://amanleek.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An insurtech startup providing a digital platform for insurance brokerage services in Egypt.", 2, false, "https://www.linkedin.com/company/amanleek/", "Cairo", "https://logo.clearbit.com/amanleek.com", "Amanleek", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "https://www.amazon.jobs/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A multinational technology company focusing on e-commerce, cloud computing, digital streaming, and artificial intelligence.", 7, false, "https://www.linkedin.com/company/amazon/", "Cairo, Giza, Alexandria", "https://logo.clearbit.com/amazon.com", "Amazon", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "https://www.appraid-tech.com/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology company focused on providing solutions for the Electric Vehicle (EV) ecosystem.", 1, false, "https://www.linkedin.com/company/appraid-llc/", "Smart Village", "https://logo.clearbit.com/appraid-tech.com", "APPRAID TECH", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "https://i.aqarmap.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading online real estate marketplace in Egypt and the MENA region, connecting buyers with sellers.", 4, false, "https://www.linkedin.com/company/aqarmap/", "Cairo", "https://logo.clearbit.com/aqarmap.com", "Aqarmap Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "https://www.areebtechnology.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology consulting firm specializing in enterprise solutions, digital transformation, and software development.", 1, false, "https://www.linkedin.com/company/areeb-technology/", "Cairo", "https://logo.clearbit.com/areebtechnology.com", "Areeb Technology", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "https://www.arpuplus.com/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading provider of mobile value-added services (VAS) and digital solutions in the MENA region.", 1, false, "https://www.linkedin.com/company/arpuplusofficial/", "Cairo", "https://logo.clearbit.com/arpuplus.com", "ArpuPlus", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "https://jobs.atos.net/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global leader in digital transformation with expertise in cybersecurity, cloud, and high-performance computing.", 1, false, "https://www.linkedin.com/company/atos/", "Cairo", "https://logo.clearbit.com/atos.net", "Atos", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "https://www.axisapp.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A financial wellness app that provides users with access to their earned wages and financial guidance.", 2, false, "https://www.linkedin.com/company/axisapp/", "Cairo", "https://logo.clearbit.com/axisapp.com", "Axis", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "https://www.bdc.com.eg/bdcwebsite/careers.html", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "One of Egypt's oldest and largest banks, providing a full range of banking services to corporate and retail customers.", 2, false, "https://www.linkedin.com/company/bdcegypt/", "Cairo, Alexandria", "https://logo.clearbit.com/bdc.com.eg", "Banque du Caire", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "http://www.banquemisr.com", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Established in 1920, Banque Misr is a pioneer in the Egyptian banking sector, offering a wide array of financial products and services.", 2, false, "https://www.linkedin.com/company/banque-misr/", "Cairo, Alexandria", "https://logo.clearbit.com/banquemisr.com", "Banque Misr", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "https://careers.beetleware.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development and technology services company that helps businesses innovate and grow through custom digital solutions.", 1, false, "https://www.linkedin.com/company/beetlewaregroup/", "Cairo", "https://logo.clearbit.com/beetleware.com", "Beetleware", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "https://biogenericpharma.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A pharmaceutical company focused on producing high-quality, affordable generic medications.", 3, false, "https://www.linkedin.com/company/biogeneric-pharma/", "Cairo", "https://logo.clearbit.com/biogenericpharma.com", "BGP", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "https://www.blackstoneeit.com/careers", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading global IT service provider, offering innovative technology solutions, consulting, and outsourcing services.", 1, false, "https://www.linkedin.com/company/blackstoneeit/", "Cairo", "https://logo.clearbit.com/blackstoneeit.com", "BlackStone eIT", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "https://www.blink22.com/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development agency that builds high-quality mobile and web applications for startups and established companies.", 1, false, "https://www.linkedin.com/company/blink22/", "Alexandria", "https://logo.clearbit.com/blink22.com", "Blink22", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "https://blnk.zenats.com/en/careers_page", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company enabling instant consumer financing for merchants at the point of sale, driving growth and financial inclusion.", 2, false, "https://www.linkedin.com/company/blnkconsumerfinance/", "Giza", "https://logo.clearbit.com/blnk.ai", "blnk", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "https://jobs.lever.co/Bosta", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A tech-enabled logistics company that provides fast and reliable last-mile delivery services for e-commerce businesses.", 8, false, "https://www.linkedin.com/company/bostaapp/", "Cairo", "https://logo.clearbit.com/bosta.co", "Bosta", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "-", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bravo is an application that allows users to shop from all stores in one place with a unified cart and checkout.", 7, false, "https://www.linkedin.com/company/bravo-shop-right/", "Cairo", "", "Bravo", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "https://www.breadfast.com/careers_categories/technology/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An online grocery delivery platform that provides scheduled and on-demand delivery of fresh bread, groceries, and household essentials.", 7, false, "https://www.linkedin.com/company/breadfast/", "Cairo", "https://logo.clearbit.com/breadfast.com", "Breadfast", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "https://brightskiesinc.com/careers", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology company delivering high-quality agile software development, with a focus on IoT, cloud solutions, and automotive software.", 1, false, "https://www.linkedin.com/company/brightskies/", "Alexandria, Smart Village", "https://logo.clearbit.com/brightskiesinc.com", "BrightSkies", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "http://www.brimore.com", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A social commerce platform that allows manufacturers to sell their products directly to consumers through a network of sellers.", 7, false, "https://www.linkedin.com/company/brimore/", "Cairo", "https://logo.clearbit.com/brimore.com", "Brimore", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "http://www.buseet.com/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A tech-based transit company offering a comfortable, affordable, and reliable bus network for daily commuters.", 8, false, "https://www.linkedin.com/company/buseet/", "Cairo", "https://logo.clearbit.com/buseet.com", "Buseet", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, "http://www.canalsugar.com", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A large-scale agricultural and industrial project focused on beet sugar production in Al Minya, Egypt.", 6, false, "https://www.linkedin.com/company/canal-sugar/", "Cairo", "https://logo.clearbit.com/canalsugar.com", "Canal Sugar", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, "https://www.cairomotive.com/careers", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company specializing in providing high-quality custom software solutions and services.", 1, false, "https://www.linkedin.com/company/cairomotive/", "Cairo", "https://logo.clearbit.com/cairomotive.com", "CairoMotive", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, "https://www.careem.com/", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The everyday Super App for the greater Middle East, offering services in ride-hailing, food delivery, payments, and more.", 8, false, "https://www.linkedin.com/company/careem/", "Alexandria, Smart Village", "https://logo.clearbit.com/careem.com", "Careem", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, "http://www.cartona.com/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A B2B e-commerce platform that connects retailers with wholesalers and manufacturers, digitizing the traditional trade market.", 7, false, "https://www.linkedin.com/company/cartona-egypt/", "Giza", "https://logo.clearbit.com/cartona.com", "Cartona", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, "https://cellula-tech.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software company providing innovative IT solutions and services to help businesses achieve digital transformation.", 1, false, "https://www.linkedin.com/company/cellula-technologies/", "Giza", "https://logo.clearbit.com/cellula-tech.com", "Cellula Technologies", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, "https://careers.cegedim.com/en", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global technology and services company specializing in the healthcare field, providing software, data flow management, and BPO services.", 1, false, "https://www.linkedin.com/company/cegedim-egypt/", "New Cairo", "https://logo.clearbit.com/cegedim.com", "Cegedim Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, "https://www.cibeg.com/en/careers", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egypt's leading private-sector bank, offering a broad range of financial products and services to its customers, including enterprises and individuals.", 2, false, "https://www.linkedin.com/company/cibegypt/", "Cairo, Alexandria", "https://logo.clearbit.com/cibeg.com", "CIB Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, "https://jobs.concentrix.com/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading global provider of customer experience (CX) solutions and technology, improving business performance for the world's best brands.", 9, false, "https://www.linkedin.com/company/concentrix/", "Smart Village", "https://logo.clearbit.com/concentrix.com", "Concentrix", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, "https://contact.eg/careers", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading provider of non-bank financial services in Egypt, offering a range of financing, insurance, and other financial products.", 2, false, "https://www.linkedin.com/company/contact-eg/", "Cairo", "https://logo.clearbit.com/contact.eg", "Contact", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, "https://cortex-innovations.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development and creative digital marketing company that provides innovative solutions for businesses.", 1, false, "https://www.linkedin.com/company/cortex-innovations-llc/", "Alexandria", "https://logo.clearbit.com/cortex-innovations.com", "Cortex Innovations LLC", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, "https://www.crewteq.com/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An IT staff augmentation company that builds dedicated remote development teams for global clients from a talent pool in Egypt.", 1, false, "https://www.linkedin.com/company/crewteq/", "Cairo", "https://logo.clearbit.com/crewteq.com", "CrewTeQ", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, "http://www.cubeconsultants.org", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An architecture, planning, and engineering consultancy firm providing comprehensive design and supervision services.", 9, false, "https://www.linkedin.com/company/cube-consultants--egypt/", "Cairo", "https://logo.clearbit.com/cubeconsultants.org", "CUBE Consultants", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, "https://careers.cyshield.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global cybersecurity company providing managed detection and response (MDR) services to help organizations combat cyber threats.", 1, false, "https://www.linkedin.com/company/cyshield/", "Cairo", "https://logo.clearbit.com/cyshield.com", "Cyshield", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, "https://www.facebook.com/profile.php?id=100086298362453", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A platform dedicated to posting and sharing daily software job vacancies available in the Egyptian market.", 12, false, "https://www.linkedin.com/company/daily-software-jobs/", "Cairo", "", "Daily Software Jobs", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, "https://www.dopay.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company that provides a platform for employers to pay unbanked workers electronically through a mobile app and debit card.", 2, false, "https://www.linkedin.com/company/dopay/", "Cairo", "https://logo.clearbit.com/dopay.com", "Dopay", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, "https://www.drive-finance.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A financial services company specializing in auto financing and leasing solutions for consumers in Egypt.", 2, false, "https://www.linkedin.com/company/drive-finance-auto/", "New cairo", "https://logo.clearbit.com/drive-finance.com", "Drive Finance", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, "https://www.dsquares.com/careers", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A full-service loyalty and rewards solutions provider, helping businesses retain customers through customized programs.", 1, false, "https://www.linkedin.com/company/dsquares/", "Cairo", "https://logo.clearbit.com/dsquares.com", "Dsquares", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, "https://www.edecs.com/career-vacancies", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading engineering, procurement, and construction company in Egypt and the Middle East, specialized in infrastructure, marine, and railway projects.", 5, false, "https://www.linkedin.com/company/edecs/", "Cairo", "https://logo.clearbit.com/edecs.com", "EDECS", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, "https://careers.efghldg.com/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A trailblazing financial institution with a universal banking platform across frontier and emerging markets.", 2, false, "https://www.linkedin.com/company/efgholding/", "Cairo", "https://logo.clearbit.com/efghldg.com", "EFG Holding", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, "https://www.eg-bank.com/En/Careers", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An Egyptian bank offering innovative banking solutions and services tailored for youth, startups, and businesses.", 2, false, "https://www.linkedin.com/company/egbankegypt/", "Giza", "https://logo.clearbit.com/eg-bank.com", "EGBANK", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, "http://www.ejad.com.eg/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An IT solutions provider that helps enterprises with digital transformation through software and system integration services.", 1, false, "https://www.linkedin.com/company/ejad/", "Nasr City", "https://logo.clearbit.com/ejad.com.eg", "Ejad", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, "https://career.ejada.com/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading IT services & solutions provider in the Middle East & Africa, enabling enterprises to maintain a competitive edge.", 1, false, "https://www.linkedin.com/company/ejada/about/", "Cairo", "https://logo.clearbit.com/ejada.com", "Ejada", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, "https://careers.elarabygroup.com/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading Egyptian enterprise in manufacturing and marketing of electronic and home appliances.", 6, false, "https://www.linkedin.com/company/elarabygroup/about/", "Cairo", "https://logo.clearbit.com/elarabygroup.com", "El-Araby", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, "https://elhazek.com/en/careers/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading construction company in Egypt and the region, providing integrated services in engineering, procurement, and construction.", 5, false, "https://www.linkedin.com/company/egyptian-union-for-construction-elhazek/", "Cairo", "https://logo.clearbit.com/elhazek.com", "El Hazek Construction", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, "https://elevateholding.net/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A holding company that builds and fosters businesses in various sectors, aiming to create value and drive innovation.", 12, false, "https://www.linkedin.com/company/elevateholding/", "Cairo", "https://logo.clearbit.com/elevateholding.net", "Elevate Holding", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, "https://elmenus.recruitee.com/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comprehensive food discovery and ordering platform in Egypt, helping people decide what to eat and from where.", 7, false, "https://www.linkedin.com/company/elmenus.com/", "Cairo", "https://logo.clearbit.com/elmenus.com", "elmenus", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, "https://elsewedyelectric.com/en/careers", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global leader in integrated energy solutions, cables, and electrical products, with a strong presence in Africa and the Middle East.", 6, false, "https://www.linkedin.com/company/elsewedyelectric/", "Cairo", "https://logo.clearbit.com/elsewedyelectric.com", "ElSewedy Electric", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, "http://www.emaarmisr.com", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading developer of premium lifestyle communities in Egypt, creating world-class integrated developments.", 4, false, "https://www.linkedin.com/company/emaar-misr/", "Cairo", "https://logo.clearbit.com/emaarmisr.com", "Emaar Misr", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, "https://embeddedmeetup.net/", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A community for embedded systems enthusiasts and professionals in Egypt to connect, learn, and share knowledge.", 12, false, "https://www.linkedin.com/company/embeddedmeetup/", "Cairo", "https://logo.clearbit.com/embeddedmeetup.net", "Embedded Meetup", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, "https://enozom.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company providing custom web and mobile application development, as well as software testing services.", 1, false, "https://www.linkedin.com/company/enozom/", "Alexandria", "https://logo.clearbit.com/enozom.com", "Enozom Software", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, "https://careers.eprom.com.eg/en/Careers/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading provider of operation and maintenance management services for the oil, gas, and petrochemical industries.", 6, false, "https://www.linkedin.com/company/eprom/", "Alexandria", "https://logo.clearbit.com/eprom.com.eg", "Egyptian Projects Operation and Maintenance (EPROM)", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, "https://www.eraasoft.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software company that provides professional training courses in software development and offers web and mobile app development services.", 1, false, "https://linkedin.com/company/eraasoft/", "Cairo", "https://logo.clearbit.com/eraasoft.com", "EraaSoft", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, "https://www.ericsson.com/en/careers", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global leader in communication technology and services, enabling the full value of connectivity for service providers.", 1, false, "https://www.linkedin.com/company/ericsson/", "Smart Village", "https://logo.clearbit.com/ericsson.com", "Ericsson", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, "https://espace.com.eg/jobs/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company providing a wide range of services including web development, mobile apps, and enterprise solutions.", 1, false, "https://www.linkedin.com/company/espace/", "Alexandria", "https://logo.clearbit.com/espace.com.eg", "Espace", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, "https://careers.etisalat.ae/en/index.html", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "e& (formerly Etisalat Group) is one of the world’s leading technology and investment groups, creating a brighter, digital future.", 1, false, "https://www.linkedin.com/company/eanduae/", "Smart Village", "https://logo.clearbit.com/eand.com", "Etisalat", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, "https://www.evapharma.com/careers", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "One of the fastest-growing pharmaceutical companies in the MENA region, focused on improving patient health and quality of life.", 3, false, "https://www.linkedin.com/company/eva-pharma/", "Giza", "https://logo.clearbit.com/evapharma.com", "EVA pharma", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, "https://eventumsolutions.com/careers/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software house that specializes in developing innovative web and mobile applications for various industries.", 1, false, "https://www.linkedin.com/company/eventum-solutions/", "Smouha", "https://logo.clearbit.com/eventumsolutions.com", "Eventum Solutions", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, "https://www.expandcart.com", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading e-commerce platform in the MENA region that allows individuals and businesses to create online stores quickly and easily.", 7, false, "https://www.linkedin.com/company/expandcart/", "Cairo", "https://logo.clearbit.com/expandcart.com", "ExpandCart", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, "https://extremesolution.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development and technology consulting company that delivers cutting-edge solutions for businesses worldwide.", 1, false, "https://www.linkedin.com/company/extreme-solution/", "Cairo", "https://logo.clearbit.com/extremesolution.com", "Extreme Solution", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, "http://www.faturab2b.com", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A B2B marketplace that connects wholesalers with retailers in the FMCG industry, offering a seamless ordering process.", 7, false, "https://www.linkedin.com/company/fatura-فاتورة/", "Cairo", "https://logo.clearbit.com/faturab2b.com", "Fatura", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, "https://fawry.com/careers/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The leading Egyptian Digital Transformation & E-Payments Platform, offering financial services to consumers and businesses.", 2, false, "https://www.linkedin.com/company/fawry/", "Cairo", "https://logo.clearbit.com/fawry.com", "Fawry", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, "https://fekra-egy.com/contact-us/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An Odoo Gold Partner in Egypt providing ERP solutions and business management software services to streamline operations.", 1, false, "https://www.linkedin.com/company/fekra-egy/", "Cairo", "https://logo.clearbit.com/fekra-egy.com", "Fekra Technologies", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, "https://flairstech.com/careers", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An international IT and Software Services company providing business solutions, software development, and BPO services.", 1, false, "https://www.linkedin.com/company/flairstech/", "Cairo", "https://logo.clearbit.com/flairstech.com", "Flairs Tech", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, "https://www.foodics.com/careers/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading Restaurant-Tech company in MENA that provides an all-in-one POS and restaurant management system.", 1, false, "https://www.linkedin.com/company/foodics/", "Cairo", "https://logo.clearbit.com/foodics.com", "Foodics", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, "https://www.forsaegypt.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Buy-Now-Pay-Later (BNPL) service provider that allows customers to purchase goods and services on installment plans.", 2, false, "https://www.linkedin.com/company/forsaegypt/", "New Cairo", "https://logo.clearbit.com/forsaegypt.com", "Forsa", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76, "https://www.froneri.com/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global ice cream company, producing iconic brands and creating delicious ice cream for people all over the world.", 6, false, "https://www.linkedin.com/company/froneriice-creamegypt/", "Cairo", "https://logo.clearbit.com/froneri.com", "FRONERI", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, "https://careers.gama.com.eg/", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading private construction company in Egypt providing integrated engineering, procurement, and construction services.", 5, false, "https://www.linkedin.com/company/gamaconstructionegypt/", "Cairo", "https://logo.clearbit.com/gama.com.eg", "Gama Construction", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78, "https://garraio.com/careers/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology company specializing in IoT, AI, and custom software solutions to empower businesses.", 1, false, "https://www.linkedin.com/company/garraiollc/", "Cairo", "https://logo.clearbit.com/garraio.com", "Garraio, LLC", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79, "https://gatesdevelopments.com/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A real estate development company in Egypt, aiming to deliver unique and high-quality residential and commercial projects.", 4, false, "https://www.linkedin.com/company/gatesdevelopments/", "Giza", "https://logo.clearbit.com/gatesdevelopments.com", "Gates Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, "https://geidea.net", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading fintech company offering digital payment solutions, POS terminals, and payment gateways for businesses.", 2, false, "https://www.linkedin.com/company/geidea/", "Cairo", "https://logo.clearbit.com/geidea.net", "geidea", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81, "https://getpayin.com/", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A financial technology company that provides online payment gateway solutions for businesses in the MENA region.", 2, false, "https://www.linkedin.com/company/getpayin/", "Cairo", "https://logo.clearbit.com/getpayin.com", "GetPayIn", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82, "https://www.gizasystemscareers.com/en/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading systems integrator in the MEA region, designing and deploying industry-specific technology solutions.", 1, false, "https://www.linkedin.com/company/giza-systems/", "Cairo", "https://logo.clearbit.com/gizasystems.com", "Giza Systems", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83, "https://www.gt-ict.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An ICT solutions provider offering services in infrastructure, managed services, and digital transformation.", 1, false, "https://www.linkedin.com/company/gt-global-technologies/", "Unknown", "https://logo.clearbit.com/gt-ict.com", "Global Technologies", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, "http://www.goodsmartegypt.com", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An online grocery service that provides employees of contracted companies with groceries on a monthly subscription basis.", 7, false, "https://www.linkedin.com/company/goodsmart-egypt/", "Cairo", "https://logo.clearbit.com/goodsmartegypt.com", "Goodsmart", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 85, "https://www.goodzii.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An e-commerce platform offering a curated selection of products across various categories, focusing on quality and unique items.", 7, false, "https://www.linkedin.com/company/goodzii/", "Cairo", "https://logo.clearbit.com/goodzii.com", "Goodzii", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86, "https://halan.com/", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Part of MNT-Halan, Egypt's leading fintech ecosystem, offering services from ride-hailing and delivery to digital payments and loans.", 8, false, "https://www.linkedin.com/company/halan/", "Giza", "https://logo.clearbit.com/mnt-halan.com", "Halan", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87, "https://www.hassanallam.com/careers", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "One of the largest privately owned corporations in Egypt and the MENA region, specializing in engineering, construction, and infrastructure.", 5, false, "https://www.linkedin.com/company/hassan-allam-holding/", "Cairo", "https://logo.clearbit.com/hassanallam.com", "Hassan Allam Holding", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88, "https://www.henkel.com/careers", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global leader in adhesive technologies and consumer brands, including laundry, home care, and beauty products.", 6, false, "https://www.linkedin.com/company/henkel/", "Cairo", "https://logo.clearbit.com/henkel.com", "Henkel", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89, "https://careers.hpe.com/us/en", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The global edge-to-cloud company built to transform businesses by helping them connect, protect, analyze, and act on all their data.", 1, false, "https://www.linkedin.com/company/hewlett-packard-enterprise/", "Smart Village", "https://logo.clearbit.com/hpe.com", "Hewlett Packard Enterprise", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90, "https://www.hikma.com/careers/", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global pharmaceutical company focused on developing, manufacturing and marketing a broad range of branded and non-branded generic medicines.", 3, false, "https://www.linkedin.com/company/hikma-pharmaceuticals/", "Cairo", "https://logo.clearbit.com/hikma.com", "Hikma Pharmaceuticals", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91, "https://careers.smartrecruiters.com/Homzmart", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading e-commerce marketplace for furniture and home goods, connecting consumers with a wide range of manufacturers and brands.", 7, false, "https://www.linkedin.com/company/homzmart/", "Cairo", "https://logo.clearbit.com/homzmart.com", "Homzmart", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92, "https://career.huawei.com/reccampportal/portal5/index.html", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading global provider of information and communications technology (ICT) infrastructure and smart devices.", 1, false, "https://www.linkedin.com/company/huawei/", "Smart Village", "https://logo.clearbit.com/huawei.com", "Huawei", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93, "https://careers.hpd.com.eg/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A prominent real estate developer in Egypt, renowned for creating large-scale, high-quality integrated communities.", 4, false, "https://www.linkedin.com/company/hydepark-developments/", "Cairo", "https://logo.clearbit.com/hpd.com.eg", "Hyde Park Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94, "https://www.ibm.com/careers", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global technology company that provides hardware, software, cloud-based services, and cognitive computing.", 1, false, "https://www.linkedin.com/company/ibm/", "Smart Village", "https://logo.clearbit.com/ibm.com", "IBM", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95, "https://www.implicit.cloud/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A cloud-native services company that helps businesses design, build, and manage their cloud infrastructure and applications.", 1, false, "https://www.linkedin.com/company/implicit-cloud/", "Cairo", "https://logo.clearbit.com/implicit.cloud", "Implicit", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96, "https://www.incorta.com/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An all-in-one data and analytics platform that unifies complex data sources and provides a single source of truth for business insights.", 1, false, "https://www.linkedin.com/company/incorta/", "Sidi Gaber & New Cairo", "https://logo.clearbit.com/incorta.com", "Incorta", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97, "https://inovaeg.com/jobs/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software solutions provider specializing in GIS, enterprise resource planning, and custom application development.", 1, false, "https://www.linkedin.com/company/inovaeg/", "Alexandria", "https://logo.clearbit.com/inovaeg.com", "Inova", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98, "https://www.instabug.com/careers", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A platform providing real-time contextual insights for mobile apps through bug reporting, crash reporting, and user feedback.", 1, false, "https://www.linkedin.com/company/instabug/", "Cairo", "https://logo.clearbit.com/instabug.com", "InstaBug", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99, "https://instashop.com/en-ae/careers", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An online grocery delivery service that allows users to order from nearby supermarkets and get their items delivered quickly.", 7, false, "https://www.linkedin.com/company/instashop-convenience-delivered/", "Cairo", "https://logo.clearbit.com/instashop.com", "instashop", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, "https://www.ischooltech.com/eg/home-ar", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An educational technology company offering accredited STEM learning programs for kids and teens in fields like programming and AI.", 10, false, "https://www.linkedin.com/company/ischooltech/", "New Cairo", "https://logo.clearbit.com/ischooltech.com", "ischool", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 101, "https://www.isekai-code.com/", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company focused on building innovative and high-quality web and mobile applications for clients.", 1, false, "https://www.linkedin.com/company/isekai-code/", "Maadi", "https://logo.clearbit.com/isekai-code.com", "Isekai Code", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, "https://iti.gov.eg/home", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egypt's national Information Technology Institute, which provides professional development and training in ICT fields.", 10, false, "https://www.linkedin.com/school/information-technology-institute-iti-/", "Smart Village", "https://logo.clearbit.com/iti.gov.eg", "ITI", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 103, "https://itida.gov.eg/English/Careers/Pages/default.aspx", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Information Technology Industry Development Agency, a governmental entity responsible for growing and developing the IT industry in Egypt.", 11, false, "https://www.linkedin.com/company/itida/", "Smart Village", "https://logo.clearbit.com/itida.gov.eg", "Itida", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 104, "https://www.itworx.com/jobs/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global software professional services company that helps businesses with their digital transformation through innovative solutions.", 1, false, "https://www.linkedin.com/company/itworx/", "Cairo", "https://logo.clearbit.com/itworx.com", "ITWorx", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 105, "https://iwandevelopments.com/careers/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A real estate development company in Egypt focused on creating unique, high-end residential communities.", 4, false, "https://www.linkedin.com/company/iwan-developments/", "Cairo", "https://logo.clearbit.com/iwandevelopments.com", "IWAN Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 106, "https://www.jtexpress-eg.com/joinus", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global logistics and express delivery company providing fast, reliable, and technology-based shipping services.", 8, false, "https://www.linkedin.com/company/jtexpress-eg/", "Cairo", "https://logo.clearbit.com/jtexpress-eg.com", "J&T Express Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 107, "https://group.jumia.com/careers?location=egypt", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading Pan-African e-commerce platform, connecting millions of consumers and sellers through its marketplace, logistics, and payment services.", 7, false, "https://www.linkedin.com/company/jumia-egypt/", "New Cairo", "https://logo.clearbit.com/jumia.com.eg", "Jumia", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 108, "https://kadesigns-eg.com/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An architectural and engineering consulting firm that offers integrated design services for a wide range of projects.", 9, false, "https://www.linkedin.com/company/ka-designs/", "Cairo", "https://logo.clearbit.com/kadesigns-eg.com", "Kader & Associates Designs", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 109, "https://www.karmsolar.com/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A solar technology and integration company that delivers innovative solar solutions to the industrial, commercial, and agricultural sectors in Egypt.", 6, false, "https://www.linkedin.com/company/karmsolar/", "Cairo", "https://logo.clearbit.com/karmsolar.com", "KarmSolar", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 110, "https://www.careers-page.com/kayan-group", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading automotive group in Egypt, the sole importer of several international car brands including SEAT, SKODA, and Volkswagen.", 7, false, "https://www.linkedin.com/company/kayan-group-automotive/", "Alexandria, Cairo", "https://logo.clearbit.com/kayan-egypt.com", "KAYAN Group", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 111, "https://store.kazyonplus.com/store/kazyon/en/joinus", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The largest hard-discounter retail chain in Egypt, offering basic consumer goods at affordable prices through a network of stores.", 7, false, "https://www.linkedin.com/company/kazyon/", "Cairo", "https://logo.clearbit.com/kazyonplus.com", "Kazyon", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 112, "https://www.kbarchitects.org/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An architectural firm providing design, planning, and consultancy services for a variety of building projects.", 9, false, "https://www.linkedin.com/company/kbarch/", "Cairo", "https://logo.clearbit.com/kbarchitects.org", "KB Architects", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 113, "https://khazenly.com/en/jobs/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A tech-enabled logistics platform offering on-demand warehousing and fulfillment solutions for businesses of all sizes.", 8, false, "https://www.linkedin.com/company/khazenly/", "Cairo", "https://logo.clearbit.com/khazenly.com", "Khazenly", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 114, "http://www.khazna.app", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A financial super app that provides digital financial services, including salary advance, bill payments, and a savings feature.", 2, false, "https://www.linkedin.com/company/khazna/", "Cairo", "https://logo.clearbit.com/khazna.app", "Khazna", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 115, "https://kinnovia.com/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A German-Egyptian software company that provides high-quality, custom software solutions and builds dedicated development teams.", 1, false, "https://www.linkedin.com/company/kinnovia-gmbh/about/", "Cairo", "https://logo.clearbit.com/kinnovia.com", "KINNOVIA", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 116, "http://www.klivvr.com", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company offering a smart payment app and card designed to provide a seamless and secure financial experience for the youth.", 2, false, "https://www.linkedin.com/company/klivvr/", "Cairo", "https://logo.clearbit.com/klivvr.com", "Klivvr", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 117, "https://www.kredit.com.eg/en/careers/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A financial services company that provides a range of financing solutions and support for Small and Medium Enterprises (SMEs) in Egypt.", 2, false, "https://www.linkedin.com/company/kredit-for-smes-finance/", "Cairo", "https://logo.clearbit.com/kredit.com.eg", "Kredit", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 118, "https://ldppartners.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An international design and consulting firm specializing in architecture, planning, engineering, and landscape design.", 9, false, "https://www.linkedin.com/company/ldp-partners/", "Cairo", "https://logo.clearbit.com/ldppartners.com", "Ldp+Partners", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 119, "https://linahfarms.com/pages/careers", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A producer of premium quality, natural, and healthy food products, specializing in dairy, beverages, and other farm-fresh goods.", 6, false, "https://www.linkedin.com/company/linahfarms/", "Giza", "https://logo.clearbit.com/linahfarms.com", "Linah Farms", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 120, "https://linkdevelopment.com/careers/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global technology solutions provider delivering innovative software, mobile development, and AI solutions to businesses.", 1, false, "https://www.linkedin.com/company/link-development/", "Cairo", "https://logo.clearbit.com/linkdevelopment.com", "Link Development", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 121, "https://livitstudio.io/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An international design studio specializing in architecture, interior design, and branding for hospitality and F&B concepts.", 9, false, "https://www.linkedin.com/showcase/livit-studio/", "Giza", "https://logo.clearbit.com/livitstudio.io", "Livit Studio", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 122, "https://career.luxoft.com/jobs", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A DXC Technology Company and a global digital strategy and software engineering firm providing bespoke technology solutions.", 1, false, "https://www.linkedin.com/company/luxoft-egypt/", "New Cairo", "https://logo.clearbit.com/luxoft.com", "Luxoft Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 123, "http://www.msquaredev.com", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A real estate development company focused on creating innovative and sustainable communities in Egypt.", 4, false, "https://www.linkedin.com/company/msquaredev/", "Giza", "https://logo.clearbit.com/msquaredev.com", "M squared", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 124, "https://www.madinetmasr.com/en/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading urban communities developer in Egypt with a long history of creating large-scale, integrated real estate projects.", 4, false, "https://www.linkedin.com/company/madinetmasr/", "Cairo", "https://logo.clearbit.com/madinetmasr.com", "Madinet Masr", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 125, "https://careers.marakez.net/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading Egyptian real estate developer with a focus on creating mixed-use properties and vibrant commercial centers.", 4, false, "https://www.linkedin.com/company/marakez-careers/", "Cairo", "https://logo.clearbit.com/marakez.net", "Marakez", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 126, "https://www.maxab.io/#careers", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A B2B e-commerce platform that connects informal food and grocery retailers with suppliers through a user-friendly app.", 7, false, "https://www.linkedin.com/company/maxab/", "Cairo", "https://logo.clearbit.com/maxab.io", "MaxAB", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 127, "https://www.mckinsey.com/middle-east/careers/careers-in-the-middle-east", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global management consulting firm that serves leading businesses, governments, and organizations to achieve lasting improvements.", 9, false, "https://www.linkedin.com/company/mckinsey/", "New Cairo", "https://logo.clearbit.com/mckinsey.com", "McKinsey & Company", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 128, "https://www.mcsoil.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An engineering services company specializing in marine construction, subsea services, and solutions for the oil and gas industry.", 5, false, "https://www.linkedin.com/company/marine-construction-services/", "Cairo", "https://logo.clearbit.com/mcsoil.com", "MCS", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 129, "https://menaalliances.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A consulting firm that provides business development, strategic partnerships, and market entry services in the MENA region.", 9, false, "https://www.linkedin.com/company/menaalliances/", "Alexandria", "https://logo.clearbit.com/menaalliances.com", "MENA ALLIANCES", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 130, "https://careers.microsoft.com/v2/global/en/home.html", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global technology leader that enables digital transformation for the era of an intelligent cloud and an intelligent edge.", 1, false, "https://www.linkedin.com/company/microsoft/", "Smart Village", "https://logo.clearbit.com/microsoft.com", "Microsoft", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 131, "https://www.misritaliaproperties.com/careers", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading real estate developer in Egypt, known for creating iconic residential, commercial, and hospitality projects.", 4, false, "https://www.linkedin.com/company/misritaliaproperties/", "Cairo", "https://logo.clearbit.com/misritaliaproperties.com", "Misr Italia Properties", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 132, "https://mnt-halan.com/careers", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egypt's leading fintech ecosystem and the largest non-bank lender to the unbanked and underbanked.", 2, false, "https://www.linkedin.com/company/mnt-halan/", "Cairo", "https://logo.clearbit.com/mnt-halan.com", "MNT-Halan", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 133, "https://www.modeso.ch/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Swiss software company that designs and develops custom web and mobile applications with development centers in Egypt.", 1, false, "https://www.linkedin.com/company/modeso/", "Alexandria, Cairo", "https://logo.clearbit.com/modeso.ch", "Modeso", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 134, "https://mogo-eg.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company providing affordable financing solutions for used cars and other assets in Egypt.", 2, false, "https://www.linkedin.com/company/mogo-eg/", "Giza", "https://logo.clearbit.com/mogo-eg.com", "Mogo", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 135, "https://www.moneyfellows.com/en-us/careers-page/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech platform that digitizes and scales traditional money circles (ROSCAs or Gam'eya) for users.", 2, false, "https://www.linkedin.com/company/moneyfellows/", "Cairo", "https://logo.clearbit.com/moneyfellows.com", "Money Fellows", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 136, "https://www.mountainviewegypt.com/career", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading Egyptian real estate development company specializing in high-end residential communities and resorts.", 4, false, "https://www.linkedin.com/company/mountainvieweg/", "Cairo", "https://logo.clearbit.com/mountainviewegypt.com", "Mountain View", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 137, "https://mozare3.net/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An agri-fintech platform that provides smallholder farmers with access to financing, markets, and agronomy support.", 2, false, "https://www.linkedin.com/company/mozare3/", "6 of October, Giza", "https://logo.clearbit.com/mozare3.net", "Mozare3", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 138, "https://www.mylerz.com/career", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A tech-driven logistics and fulfillment company providing innovative last-mile delivery and e-commerce solutions.", 8, false, "https://www.linkedin.com/company/mylerz-co/", "Cairo", "https://logo.clearbit.com/mylerz.com", "mylerz Co", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 139, "https://www.nagarro.com", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global digital engineering leader, helping clients become innovative, digital-first companies and thus win in their markets.", 1, false, "https://www.linkedin.com/company/nagarro/", "New Cairo", "https://logo.clearbit.com/nagarro.com", "Nagarro", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 140, "https://hiring.nana.co/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading online grocery delivery platform in the MENA region that connects customers with their favorite supermarkets.", 7, false, "https://www.linkedin.com/company/nana-app/", "Cairo", "https://logo.clearbit.com/nana.sa", "Nana", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 141, "https://naqla.xyz/careers/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology platform that connects truck drivers with cargo owners, revolutionizing road freight and logistics in Egypt.", 8, false, "https://www.linkedin.com/company/naqla/", "Cairo", "https://logo.clearbit.com/naqla.xyz", "Naqla", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 142, "http://www.nbe.com.eg", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The oldest and largest commercial bank in Egypt, providing a comprehensive range of corporate and retail banking services.", 2, false, "https://www.linkedin.com/company/national-bank-of-egypt/", "Cairo", "https://logo.clearbit.com/nbe.com.eg", "National Bank of Egypt (NBE)", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 143, "https://nawah-scientific.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An online platform offering on-demand scientific and analytical services for researchers and industrial clients in the MENA region.", 3, false, "https://www.linkedin.com/company/nawah-scientific/", "Cairo", "https://logo.clearbit.com/nawah-scientific.com", "Nawah Scientific", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 144, "https://apply.workable.com/nawy-real-estate/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A property technology company and real estate brokerage that helps clients find their dream homes through a smart, data-driven platform.", 4, false, "https://www.linkedin.com/company/nawyestate/", "New Cairo", "https://logo.clearbit.com/nawy.com", "Nawy", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 145, "http://getnexta.com", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company providing a next-generation banking app and payment card to help users manage their money effortlessly.", 2, false, "https://www.linkedin.com/company/nextaegypt/", "Cairo", "https://logo.clearbit.com/getnexta.com", "Nexta", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 146, "https://nile-developments.com/en/career/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A real estate development company specializing in creating iconic, high-rise buildings and luxury projects in Egypt.", 4, false, "https://www.linkedin.com/company/nile-developments/", "Cairo", "https://logo.clearbit.com/nile-developments.com", "Nile Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 147, "https://nogood.io", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A growth marketing agency that partners with brands to rapidly scale their growth through a data-driven, experimental approach.", 9, false, "https://www.linkedin.com/company/nogood/", "Cairo", "https://logo.clearbit.com/nogood.io", "NoGood", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 148, "https://www.noon.com/", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading digital ecosystem of products and services, born in the Middle East, offering a premier e-commerce and delivery platform.", 7, false, "https://www.linkedin.com/company/nooncom/", "Cairo", "https://logo.clearbit.com/noon.com", "Noon", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 149, "https://apply.workable.com/nowlun/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A digital freight marketplace that simplifies sea shipping by connecting shippers and carriers on one platform.", 8, false, "https://www.linkedin.com/company/nowlun-com/", "Alexandria", "https://logo.clearbit.com/nowlun.com", "Nowlun.com", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 150, "https://www.nti.sci.eg/eta/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The National Telecommunication Institute of Egypt, a leading center for training, education, and research in telecommunications.", 10, false, "https://www.linkedin.com/school/nti-eg/", "Smart Village", "https://logo.clearbit.com/nti.sci.eg", "NTI", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 151, "https://www.tra.gov.eg/en/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The National Telecom Regulatory Authority of Egypt, responsible for regulating the telecommunications sector and ensuring a competitive market.", 11, false, "https://www.linkedin.com/company/ntraeg/", "Smart Village", "https://logo.clearbit.com/tra.gov.eg", "NTRA", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 152, "https://objects.ws/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development and IT consulting company that provides innovative solutions and builds dedicated teams for clients.", 1, false, "https://www.linkedin.com/company/objects/", "Alexandria", "https://logo.clearbit.com/objects.ws", "Objects", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 153, "https://www.orangedigitalcenters.com/country/EG/home", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Orange Digital Center Egypt is a hub for free digital training, incubation, and acceleration for startups, and support for project owners.", 10, false, "https://www.linkedin.com/company/orange-digital-center-egypt/", "Cairo", "https://logo.clearbit.com/orangedigitalcenters.com", "ODC", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 154, "https://oliv.finance/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company providing Sharia-compliant financial solutions, including Buy-Now-Pay-Later services, for consumers and businesses.", 2, false, "https://www.linkedin.com/company/oliv-finance/", "Cairo", "https://logo.clearbit.com/oliv.finance", "oliv", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 155, "https://www.onecommerce.group/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An e-commerce services company that builds and scales direct-to-consumer brands in the Middle East.", 7, false, "https://www.linkedin.com/company/onecommerce-group/", "Cairo", "https://logo.clearbit.com/onecommerce.group", "oneCommerce", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 156, "https://careers.oracle.com/jobs/#en/sites/jobsearch", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global technology corporation that provides a wide range of cloud-based applications, platforms, and computing infrastructure.", 1, false, "https://www.linkedin.com/company/oracle/", "Cairo", "https://logo.clearbit.com/oracle.com", "Oracle", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 157, "https://careers.orascom.com/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading global engineering and construction contractor with a footprint covering the Middle East, Africa, and the United States.", 5, false, "https://www.linkedin.com/company/orascom-construction-plc/", "Cairo", "https://logo.clearbit.com/orascom.com", "Orascom Construction PLC", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 158, "https://www.orascomdh.com/careers", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading developer of fully integrated destinations, including hotels, private villas, and apartments, with a strong focus on tourism.", 4, false, "https://www.linkedin.com/company/orascom-development/", "Cairo", "https://logo.clearbit.com/orascomdh.com", "Orascom Development", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 159, "https://www.pgcareers.com/mea/en", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global consumer goods company with a portfolio of trusted, quality, leadership brands including Pampers, Tide, and Gillette.", 6, false, "https://www.linkedin.com/company/procter-and-gamble/", "New Cairo", "https://logo.clearbit.com/pg.com", "P&G (Procter & Gamble)", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 160, "https://careers.palmhillsdevelopments.com/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading real estate developer in Egypt, creating integrated residential, commercial, and resort communities.", 4, false, "https://www.linkedin.com/company/palm-hills-developments/", "Cairo", "https://logo.clearbit.com/palmhillsdevelopments.com", "Palm Hills Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 161, "https://www.parkvillepharma.com/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A pharmaceutical company dedicated to developing and providing high-quality cosmeceutical and healthcare products.", 3, false, "https://www.linkedin.com/company/parkville-pharmaceuticals-company/", "Giza", "https://logo.clearbit.com/parkvillepharma.com", "Parkville", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 162, "https://www.paymob.com/en/careers", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading financial services enabler in MENAP, providing a comprehensive payment ecosystem for businesses to accept and manage payments.", 2, false, "https://www.linkedin.com/company/paymobcompany/", "Cairo", "https://logo.clearbit.com/paymob.com", "Paymob", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 163, "https://www.pharco.org/careers.aspx", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The largest pharmaceutical manufacturer in the Middle East and Africa, focused on research, development, and production of medicines.", 3, false, "https://www.linkedin.com/company/pharco-corporation/", "Alexandria", "https://logo.clearbit.com/pharco.org", "Pharco Corporation", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 164, "https://www.pharos-solutions.de/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A German-based IT services and consulting company with a branch in Egypt, specializing in software development and staff augmentation.", 1, false, "https://www.linkedin.com/company/pharos-solutions-ug/", "Alexandria", "https://logo.clearbit.com/pharos-solutions.de", "Pharos Solutions", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 165, "https://predevelopments.com/careers/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading real estate developer in Egypt, committed to delivering innovative and quality-driven residential and mixed-use projects.", 4, false, "https://www.linkedin.com/company/predevelopments/", "Cairo", "https://logo.clearbit.com/predevelopments.com", "PRE Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 166, "https://premiumcard.net/careers", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A financial services company offering a credit card that allows users to pay for purchases in interest-free installments.", 2, false, "https://www.linkedin.com/company/premium-card/", "Cairo", "https://logo.clearbit.com/premiumcard.net", "Premium Card", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 167, "http://www.primeholdingco.com", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading investment banking and financial services company in the MENA region, offering brokerage, asset management, and advisory services.", 2, false, "https://www.linkedin.com/company/prime-holding/", "Giza", "https://logo.clearbit.com/primeholdingco.com", "Prime Holding", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 168, "https://careers.procore.com/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading provider of cloud-based construction management software, connecting project teams from the office to the field.", 1, false, "https://www.linkedin.com/company/procore-technologies/", "Cairo", "https://logo.clearbit.com/procore.com", "Procore", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 169, "https://www.procrew.pro/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An IT staff augmentation company that helps businesses hire dedicated remote software developers and build professional teams.", 1, false, "https://www.linkedin.com/company/procrewpro/", "Alexandria", "https://logo.clearbit.com/procrew.pro", "ProCrew", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 170, "https://www.prometeon.com/EG/ar_EG", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global company solely focused on the industrial tyre business for trucks, buses, and agro applications, with manufacturing facilities in Egypt.", 6, false, "https://www.linkedin.com/company/prometeontyregroup/", "Alexandria", "https://logo.clearbit.com/prometeon.com", "PROMETEON TYRE GROUP", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 171, "https://www.pwc.com/us/en/careers.html", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global network of firms delivering assurance, tax, and advisory services to build trust in society and solve important problems.", 9, false, "https://www.linkedin.com/company/pwc-middle-east/", "Smouha & New Cairo", "https://logo.clearbit.com/pwc.com", "PWC", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 172, "https://www.qalaaholdings.com/careers", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An African leader in energy and infrastructure, building and investing in vital projects across strategic sectors.", 12, false, "https://www.linkedin.com/company/qalaa-holdings/", "Cairo", "https://logo.clearbit.com/qalaaholdings.com", "Qalaa Holdings", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 173, "https://www.qnb.com.eg/sites/qnb/qnbegypt/page/en/encareersegypt.html", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "QNB ALAHLI is one of the leading financial institutions in Egypt, providing a wide range of banking services to individuals and corporations.", 2, false, "https://www.linkedin.com/company/qnbeg/", "Cairo, Giza", "https://logo.clearbit.com/qnb.com.eg", "QNB", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 174, "https://www.rabbitmart.com/careers/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An ultra-fast grocery delivery company that promises to deliver groceries and essentials to your doorstep in minutes.", 7, false, "https://www.linkedin.com/company/rabbitmart/", "Maadi, Cairo", "https://logo.clearbit.com/rabbitmart.com", "Rabbit", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 175, "https://rabbittec.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software house based in Alexandria that specializes in building custom web and mobile applications for clients.", 1, false, "https://www.linkedin.com/company/rabbit-technology/", "Alexandria", "https://logo.clearbit.com/rabbittec.com", "Rabbit Technology", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 176, "https://www.raisa.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An investment company that acquires and manages oil and gas assets, with a significant technical and analytical presence in Cairo.", 2, false, "https://www.linkedin.com/company/raisa-energy/", "Maadi", "https://logo.clearbit.com/raisa.com", "Raisa Energy", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 177, "https://careers.rayacorp.com/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading investment conglomerate managing a diversified portfolio in IT, contact centers, smart buildings, and financial services.", 1, false, "https://www.linkedin.com/company/raya/", "6th October City", "https://logo.clearbit.com/rayacorp.com", "Raya", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 178, "https://robustagroup.com/join-us/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A digital product agency and technology consultancy that partners with businesses to design, build, and scale exceptional digital experiences.", 1, false, "https://www.linkedin.com/company/robusta-studio/", "New Cairo, Cairo", "https://logo.clearbit.com/robustagroup.com", "Robusta Studio", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 179, "https://rowad-rme.com/careers/", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading construction company in Egypt that provides integrated engineering, procurement, and construction services.", 5, false, "https://www.linkedin.com/company/rowad-modern-engineering/", "Cairo", "https://logo.clearbit.com/rowad-rme.com", "Rowad Modern Engineering", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 180, "https://www.rsa.com/rsa-careers/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global leader in cybersecurity and risk management, providing solutions for identity and access management and threat detection.", 1, false, "https://www.linkedin.com/company/rsasecurity/", "New Cairo", "https://logo.clearbit.com/rsa.com", "RSA Security", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 181, "https://www.rubikal.com/careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development firm that specializes in building scalable web and mobile applications for startups and enterprises.", 1, false, "https://www.linkedin.com/company/rubikal_llc/", "Alexandria", "https://logo.clearbit.com/rubikal.com", "Rubikal", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 182, "https://sahl.recruitee.com/#", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech application that provides a one-stop solution for paying all household bills and services electronically.", 2, false, "https://www.linkedin.com/company/sahlpayapp/", "Giza", "https://logo.clearbit.com/sahl.money", "Sahl", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 183, "https://www.saib.com.eg/en/careers/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An Egyptian bank that provides a full range of retail, corporate, and investment banking services.", 2, false, "https://www.linkedin.com/company/saib-bank/", "Giza", "https://logo.clearbit.com/saib.com.eg", "saib", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 184, "https://sanasofteg.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company that offers web and mobile app development, UI/UX design, and IT consulting services.", 1, false, "https://www.linkedin.com/company/sanasoftltd/", "Alexandria, Cairo", "https://logo.clearbit.com/sanasofteg.com", "Sana Soft", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 185, "https://www.sandoz.com/careers/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global leader in generic pharmaceuticals and biosimilars, committed to increasing access to high-quality, life-enhancing medicines.", 3, false, "https://www.linkedin.com/company/sandozegypt/", "Cairo", "https://logo.clearbit.com/sandoz.com", "Sandoz Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 186, "http://seitech-solutions.com/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An IT company providing a range of services including software development, systems integration, and technical support.", 1, false, "https://www.linkedin.com/company/seitech-solutions-eg/", "Giza", "https://logo.clearbit.com/seitech-solutions.com", "SEITech Solutions", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 187, "http://www.bseven.com", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A retail company specializing in the sale of consumer electronics, home appliances, and personal care products.", 7, false, "https://www.linkedin.com/company/bsevenegypt/", "Giza", "https://logo.clearbit.com/bseven.com", "seven", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 188, "https://myshemsi.com/en-EG/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology platform for solar energy, allowing users to find, purchase, and manage solar solutions for their homes and businesses.", 1, false, "https://www.linkedin.com/company/shemsi/", "Cairo", "https://logo.clearbit.com/myshemsi.com", "Shemsi", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 189, "http://shipblu.com/en/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A tech-enabled logistics company using AI and machine learning to provide a premium last-mile delivery experience.", 8, false, "https://www.linkedin.com/company/shipblu/", "Cairo", "https://logo.clearbit.com/shipblu.com", "ShipBlu", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 190, "https://www.siac.com.eg/?q=careers", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading regional construction company in Egypt, providing integrated engineering, procurement, and construction services.", 5, false, "https://www.linkedin.com/company/siac-construction/", "Cairo", "https://logo.clearbit.com/siac.com.eg", "SIAC Construction", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 191, "https://www.siemens.com/global/en/company/jobs.html", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global technology powerhouse that focuses on intelligent infrastructure for buildings and distributed energy systems.", 1, false, "https://www.linkedin.com/company/siemens/", "New Cairo", "https://logo.clearbit.com/siemens.com", "Siemens", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 192, "https://silicon-mind.com/careers/", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company offering services in web development, mobile applications, and enterprise solutions.", 1, false, "https://www.linkedin.com/company/siliconmind/", "Alexandria", "https://logo.clearbit.com/silicon-mind.com", "Silicon Mind", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 193, "https://www.smartera3s.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An Odoo partner providing comprehensive ERP system implementation, customization, and support for businesses.", 1, false, "https://www.linkedin.com/company/smartera-3s-solutions-and-systems/", "Alexandria", "https://logo.clearbit.com/smartera3s.com", "Smartera 3S Solutions and Systems", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 194, "https://careers.sodic.com/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "One of Egypt's leading real estate development companies, building large, mixed-use communities and residential projects.", 4, false, "https://www.linkedin.com/company/sodic/", "Cairo", "https://logo.clearbit.com/sodic.com", "Sodic", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 195, "https://sonnen.eg/careers/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global market leader in smart energy storage systems and innovative energy services for households and small businesses.", 6, false, "https://www.linkedin.com/company/sonnen-egypt/", "New Cairo", "https://logo.clearbit.com/sonnen.de", "sonnen", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 196, "https://souhoola.com/en/career", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Buy-Now-Pay-Later (BNPL) fintech company that offers installment payment options for consumers at a network of merchants.", 2, false, "https://www.linkedin.com/company/souhoola/", "Cairo", "https://logo.clearbit.com/souhoola.com", "Souhoola", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 197, "http://www.speedaf.com", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A logistics service provider specializing in express delivery, freight, and warehousing solutions across Africa and the Middle East.", 8, false, "https://www.linkedin.com/company/speedafegypt/", "Cairo", "https://logo.clearbit.com/speedaf.com", "Speedaf Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 198, "https://career.sprint.xyz/jobs/Careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology-driven company offering on-demand, same-day delivery services for businesses and individuals.", 8, false, "https://www.linkedin.com/company/sprint-logistics-eg/", "Cairo", "https://logo.clearbit.com/sprint.xyz", "sprint", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 199, "https://careers.sprints.ai/jobs/Careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An EdTech company delivering talent-as-a-service through guaranteed hiring programs in various software fields.", 10, false, "https://www.linkedin.com/company/sprintsai/", "Cairo", "https://logo.clearbit.com/sprints.ai", "Sprints", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 200, "https://www.sshic.com/careers/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "One of the leading master planning, infrastructure, building design, and construction supervision firms in the Middle East.", 9, false, "https://www.linkedin.com/company/ssh-international/", "Cairo", "https://logo.clearbit.com/sshic.com", "SSH Design", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 201, "https://www.sumerge.com/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading technology solutions provider in the MEA region, offering software development and systems integration services.", 1, false, "https://www.linkedin.com/company/sumerge/", "Cairo", "https://logo.clearbit.com/sumerge.com", "Sumerge", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 202, "http://www.swift-act.com", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company that builds high-end solutions for web, mobile, and IoT platforms.", 1, false, "https://www.linkedin.com/company/swift-act/", "Cairo, Assuit", "https://logo.clearbit.com/swift-act.com", "SWIFT ACT", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 203, "https://www.swvl.com/", "1,001-5,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global provider of transformative tech-enabled mass transit solutions, offering intercity, intracity, and B2B transportation.", 8, false, "https://www.linkedin.com/company/swvl/", "Cairo", "https://logo.clearbit.com/swvl.com", "Swvl", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 204, "https://jobs.lever.co/sylndr", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An automotive e-commerce marketplace for buying, selling, and financing used cars in Egypt.", 7, false, "https://www.linkedin.com/company/sylndr/", "Cairo", "https://logo.clearbit.com/sylndr.com", "Sylndr", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 205, "https://sympl.ai/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company offering a save-your-money and buy-now-pay-later platform, allowing customers to pay in short-term, interest-free installments.", 2, false, "https://www.linkedin.com/company/symplfintech/", "Cairo", "https://logo.clearbit.com/sympl.ai", "Sympl", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 206, "https://synapseanalytics.recruitee.com/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An AI technology company that provides a machine learning operations (MLOps) platform to help businesses build and manage AI models.", 1, false, "https://www.linkedin.com/company/synapse-analytics/", "Cairo", "https://logo.clearbit.com/synapse-analytics.io", "Synapse Analytics", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 207, "https://www.taager.com/sa/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A social commerce platform that provides online sellers with a complete solution including product sourcing, warehousing, and last-mile delivery.", 7, false, "https://www.linkedin.com/company/taagercom/", "Cairo", "https://logo.clearbit.com/taager.com", "Taager", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 208, "https://tabby.ai/en-AE/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading shopping and payments app in the MENA region, offering flexible payment options including Buy-Now-Pay-Later.", 2, false, "https://www.linkedin.com/company/tabbypay/", "Cairo", "https://logo.clearbit.com/tabby.ai", "Tabby", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 209, "http://www.takka.me", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A company focused on developing sustainable energy solutions and promoting renewable energy adoption.", 12, false, "https://www.linkedin.com/company/takka-me/", "Cairo", "https://logo.clearbit.com/takka.me", "Takka", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 210, "https://talaatmoustafa.com/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading conglomerate in real estate and tourism development in Egypt, known for creating large, integrated urban communities.", 4, false, "https://www.linkedin.com/company/talaat-moustafa-group/", "Cairo", "https://logo.clearbit.com/talaatmoustafa.com", "Talaat Moustafa Group", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 211, "https://careers.deliveryhero.com/talabat", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading online food and grocery delivery platform in the MENA region, connecting users with thousands of restaurants and stores.", 7, false, "https://www.linkedin.com/company/talabat-com/", "Cairo", "https://logo.clearbit.com/talabat.com", "talabat", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 212, "https://tamara.co/en-SA/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading shopping and payments platform in the MENA region, providing Buy-Now-Pay-Later and other financial services.", 2, false, "https://www.linkedin.com/company/tamara/", "Cairo", "https://logo.clearbit.com/tamara.co", "Tamara", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 213, "https://www.tatweermisr.com/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An Egyptian real estate development company renowned for building innovative, sustainable, and high-quality mixed-use communities.", 4, false, "https://www.linkedin.com/company/tatweer-misr/", "Cairo", "https://logo.clearbit.com/tatweermisr.com", "Tatweer Misr", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 214, "https://jobs.lever.co/telda.app", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company offering a money app and payment card that simplifies sending, spending, and saving money for users in Egypt.", 2, false, "https://www.linkedin.com/company/telda/", "Cairo", "https://logo.clearbit.com/telda.app", "Telda", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 215, "https://www.te.eg/wps/portal/te/About/Careers/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The primary telephone company in Egypt, providing a full range of integrated telecommunications services, including fixed-line, mobile, and data.", 1, false, "https://www.linkedin.com/company/telecom-egypt/", "Smart Village", "https://logo.clearbit.com/te.eg", "Telecom Egypt", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 216, "https://thndr-talent.freshteam.com/jobs", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A digital investment platform that makes it easy to invest in stocks, bonds, and mutual funds directly from a mobile phone.", 2, false, "https://www.linkedin.com/company/thndrapp/", "Cairo", "https://logo.clearbit.com/thndr.app", "Thndr", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 217, "https://tradeline-stores.com/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The first and largest Apple Premium Reseller in Egypt, offering the full range of Apple products and accessories.", 7, false, "https://www.linkedin.com/company/tradeline-stores/", "Cairo", "https://logo.clearbit.com/tradeline-stores.com", "Tradeline Stores", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 218, "https://www.trella.app/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology platform that operates as a digital freight marketplace, connecting shippers with carriers to move cargo.", 8, false, "https://www.linkedin.com/company/trellaapp/", "Cairo", "https://logo.clearbit.com/trella.app", "Trella", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 219, "https://trianglz.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software development company that builds high-quality, scalable mobile and web applications for startups and enterprises.", 1, false, "https://www.linkedin.com/company/trianglz/about/", "Alexandria", "https://logo.clearbit.com/trianglz.com", "TrianglZ LLC", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 220, "https://trufinance.app/careers", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fintech company offering a Buy-Now-Pay-Later platform specifically designed for B2B transactions and business procurement.", 2, false, "https://www.linkedin.com/company/truapp/", "Cairo", "https://logo.clearbit.com/trufinance.app", "TRU", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 221, "https://trukker.com/careers", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The largest digital freight network in the MENA region, connecting transporters with cargo owners for logistics services.", 8, false, "https://www.linkedin.com/company/trukkertech/", "Suez", "https://logo.clearbit.com/trukker.com", "TruKKer", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 222, "http://www.turbo.info", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A quick commerce platform specializing in the ultra-fast delivery of groceries and everyday essentials.", 7, false, "https://www.linkedin.com/company/turbologistech/", "Cairo", "https://logo.clearbit.com/turbo.info", "Turbo EG", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 223, "http://www.unifonic.com", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading cloud communications platform in the Middle East that provides SMS, voice, and other messaging solutions for businesses.", 1, false, "https://www.linkedin.com/company/unifonic/", "Cairo", "https://logo.clearbit.com/unifonic.com", "Unifonic", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 224, "https://upwyde.com/career/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A real estate development company in Egypt focused on creating high-quality, innovative commercial and residential projects.", 4, false, "https://www.linkedin.com/company/upwyde-developments/", "Cairo", "https://logo.clearbit.com/upwyde.com", "Upwyde Developments", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 225, "https://www.valeo.com/en/find-a-job-or-internship/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A global automotive supplier and technology company that designs innovative solutions for smart mobility, with a large software development center in Egypt.", 6, false, "https://www.linkedin.com/company/valeo/", "Smart Village", "https://logo.clearbit.com/valeo.com", "Valeo", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 226, "https://www.valu.com.eg", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading Buy-Now-Pay-Later (BNPL) fintech platform in the MENA region, offering accessible and affordable consumer financing solutions.", 2, false, "https://www.linkedin.com/company/valuegypt/", "Cairo", "https://logo.clearbit.com/valu.com.eg", "Valu", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 227, "https://valify-solutions.zohorecruit.com/jobs/Careers", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology company providing AI-powered Digital Identity solutions for businesses, including user verification and onboarding.", 1, false, "https://www.linkedin.com/company/valifysolutions/", "Cairo", "https://logo.clearbit.com/valifysolutions.com", "Valify Solutions", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 228, "https://careers.vezeeta.com/", "501-1,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading digital healthcare platform in the MENA region that connects patients with doctors, labs, and pharmacies.", 3, false, "https://www.linkedin.com/company/vezeeta/", "Cairo", "https://logo.clearbit.com/vezeeta.com", "Vezeeta", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 229, "https://careers.vodafone.com/", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading global telecommunications company that provides a wide range of services, including mobile, fixed-line, and IoT solutions.", 1, false, "https://www.linkedin.com/company/vodafone/", "Smart Village", "https://logo.clearbit.com/vodafone.com", "Vodafone", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 230, "https://jobs.vodafone.com/careers?query=_VOIS", "10,001+ employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A strategic arm of Vodafone Group, providing high-quality services and building technology solutions for Vodafone's global operations.", 1, false, "https://www.linkedin.com/company/vois/", "Smart Village", "https://logo.clearbit.com/vodafone.com", "_VOIS", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 231, "https://www.xceedcc.com/site/careers", "5,001-10,000 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading global provider of multilingual Business Process Outsourcing (BPO) and customer experience management services.", 9, false, "https://www.linkedin.com/company/xceed/", "Cairo", "https://logo.clearbit.com/xceedcc.com", "Xceed", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 232, "https://www.yfs-logistics.com/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology-driven logistics company (formerly Yalla Fel Sekka) providing on-demand, last-mile delivery services for businesses.", 8, false, "https://www.linkedin.com/company/yalla-fel-sekka/", "Cairo", "https://logo.clearbit.com/yfs-logistics.com", "YFS Logistics", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 233, "https://www.yodawy.com/", "201-500 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A digital pharmacy benefits platform that allows users to order medicines and personal care products from pharmacies online.", 3, false, "https://www.linkedin.com/company/yodawyapp/", "Giza", "https://logo.clearbit.com/yodawy.com", "Yodawy", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 234, "https://youssef.law/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A leading international law firm based in Egypt, specializing in arbitration, litigation, and corporate law.", 9, false, "https://www.linkedin.com/company/youssef-plus-partners/", "Cairo", "https://logo.clearbit.com/youssef.law", "YOUSSEF + PARTNERS Arbitration Leaders", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 235, "https://hashemlaw.com/career/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "One of the oldest and largest law firms in Egypt and the Middle East, providing a full range of legal services.", 9, false, "https://www.linkedin.com/company/zaki-hashem-attorneys-at-law/", "Cairo", "https://logo.clearbit.com/hashemlaw.com", "Zaki Hashem, Attorneys at Law", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 236, "https://zaldi-capital.com/careers/", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A venture capital firm focused on investing in and supporting early-stage technology startups in the MENA region.", 2, false, "https://www.linkedin.com/company/zaldi-capital/", "New Cairo", "https://logo.clearbit.com/zaldi-capital.com", "Zaldi Capital", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 237, "https://zed.dev/jobs", "11-50 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A software company building a high-performance, multiplayer code editor for collaborative software development.", 1, false, "https://www.linkedin.com/company/zeddotdev/", "Cairo", "https://logo.clearbit.com/zed.dev", "Zed Industries", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 238, "https://www.zillacapital.com/careers/", "1-10 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An investment management and financial advisory firm that focuses on creating value in startups and growing businesses.", 2, false, "https://www.linkedin.com/company/zillacapitalnew/", "Cairo", "https://logo.clearbit.com/zillacapital.com", "Zilla Capital", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 239, "https://zinad.net/jobs.html", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A technology company specializing in cybersecurity services, software development, and secure IT solutions.", 1, false, "https://www.linkedin.com/company/zinad-security-and-software-services/", "Smart Village", "https://logo.clearbit.com/zinad.net", "ZINAD IT", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 240, "https://zulficarpartners.com/careers/", "51-200 employees", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "An international corporate law and arbitration firm based in Cairo, providing top-tier legal services to a diverse client base.", 9, false, "https://www.linkedin.com/company/zulficar-and-partners-law-firm/", "Cairo", "https://logo.clearbit.com/zulficarpartners.com", "Zulficar & Partners", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "company_id",
                keyValue: 240);

            migrationBuilder.AlterColumn<byte[]>(
                name: "logo",
                table: "Companies",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "company_size",
                table: "Companies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }
    }
}
