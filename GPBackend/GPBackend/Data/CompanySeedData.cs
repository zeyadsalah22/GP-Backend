using GPBackend.Models;

namespace GPBackend.Data
{
    public static class CompanySeedData
    {
        public static Company[] GetCompanies()
        {
            return
            [
              
                new Company
                {
                    CompanyId = 1,
                    Name = "Abdelaziz Elmasry",
                    Location = "Cairo",
                    CareersLink = "https://aelmasry.com/",
                    LinkedinLink = "https://www.linkedin.com/company/aelmasry/",
                    IndustryId = 1,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/aelmasry.com",
                    Description = "Personal portfolio of a Software Engineer.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 2,
                    Name = "Advansys",
                    Location = "Cairo",
                    CareersLink = "https://www.advansys-esc.com/",
                    LinkedinLink = "https://www.linkedin.com/company/advansys-esc/",
                    IndustryId = 1,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/advansys-esc.com",
                    Description = "A leading technology solutions provider that helps businesses to digitally transform and empower their future.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 3,
                    Name = "Akam Developments",
                    Location = "Cairo",
                    CareersLink = "https://akam.com.eg/",
                    LinkedinLink = "https://www.linkedin.com/company/akam-developments/",
                    IndustryId = 4,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/akam.com.eg",
                    Description = "A real estate development company that offers an unconventional vision of living in integrated communities with a unique quality of life.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 4,
                    Name = "Al-Futtaim",
                    Location = "Cairo",
                    CareersLink = "https://www.alfuttaim.com/",
                    LinkedinLink = "https://www.linkedin.com/company/alfuttaim/",
                    IndustryId = 7,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/alfuttaim.com",
                    Description = "A diversified conglomerate operating in automotive, financial services, real estate, and retail sectors.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 5,
                    Name = "Alexandria Mineral Oils Co. (AMOC)",
                    Location = "Alexandria",
                    CareersLink = "http://amoceg.com",
                    LinkedinLink = "https://www.linkedin.com/company/amoceg/",
                    IndustryId = 6,
                    CompanySize = "1001-5000 employees",
                    LogoUrl = "https://logo.clearbit.com/amoceg.com",
                    Description = "An Egyptian company specializing in the production of essential mineral oils, paraffin wax, and other petroleum products.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 6,
                    Name = "Alexapps",
                    Location = "Alexandria",
                    CareersLink = "https://alexforprog.com/",
                    LinkedinLink = "https://www.linkedin.com/company/alexaiapps/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/alexforprog.com",
                    Description = "A software house specializing in creating custom software solutions, including mobile and web applications.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 7,
                    Name = "Algebra Ventures",
                    Location = "Cairo",
                    CareersLink = "https://www.algebraventures.com/",
                    LinkedinLink = "https://www.linkedin.com/company/algebraventures/",
                    IndustryId = 2,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/algebraventures.com",
                    Description = "A leading Egyptian venture capital firm that invests in technology and technology-enabled startups.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 8,
                    Name = "Algoriza",
                    Location = "Cairo",
                    CareersLink = "https://algoriza.com/",
                    LinkedinLink = "https://www.linkedin.com/company/algoriza/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/algoriza.com",
                    Description = "A software development company that builds world-class engineering teams to help businesses scale.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 9,
                    Name = "almentor",
                    Location = "Cairo",
                    CareersLink = "https://www.almentor.net/",
                    LinkedinLink = "https://www.linkedin.com/company/almentor/",
                    IndustryId = 10,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/almentor.net",
                    Description = "An online video e-learning platform in Arabic, offering courses and talks from mentors across various fields.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 10,
                    Name = "Amanleek",
                    Location = "Cairo",
                    CareersLink = "https://amanleek.com/",
                    LinkedinLink = "https://www.linkedin.com/company/amanleek/",
                    IndustryId = 2,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/amanleek.com",
                    Description = "An insurtech startup providing a digital platform for insurance brokerage services in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 11,
                    Name = "Amazon",
                    Location = "Cairo, Giza, Alexandria",
                    CareersLink = "https://www.amazon.jobs/",
                    LinkedinLink = "https://www.linkedin.com/company/amazon/",
                    IndustryId = 7,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/amazon.com",
                    Description = "A multinational technology company focusing on e-commerce, cloud computing, digital streaming, and artificial intelligence.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 12,
                    Name = "APPRAID TECH",
                    Location = "Smart Village",
                    CareersLink = "https://www.appraid-tech.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/appraid-llc/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/appraid-tech.com",
                    Description = "A technology company focused on providing solutions for the Electric Vehicle (EV) ecosystem.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 13,
                    Name = "Aqarmap Egypt",
                    Location = "Cairo",
                    CareersLink = "https://i.aqarmap.com/",
                    LinkedinLink = "https://www.linkedin.com/company/aqarmap/",
                    IndustryId = 4,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/aqarmap.com",
                    Description = "A leading online real estate marketplace in Egypt and the MENA region, connecting buyers with sellers.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 14,
                    Name = "Areeb Technology",
                    Location = "Cairo",
                    CareersLink = "https://www.areebtechnology.com/",
                    LinkedinLink = "https://www.linkedin.com/company/areeb-technology/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/areebtechnology.com",
                    Description = "A technology consulting firm specializing in enterprise solutions, digital transformation, and software development.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 15,
                    Name = "ArpuPlus",
                    Location = "Cairo",
                    CareersLink = "https://www.arpuplus.com/",
                    LinkedinLink = "https://www.linkedin.com/company/arpuplusofficial/",
                    IndustryId = 1,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/arpuplus.com",
                    Description = "A leading provider of mobile value-added services (VAS) and digital solutions in the MENA region.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 16,
                    Name = "Atos",
                    Location = "Cairo",
                    CareersLink = "https://jobs.atos.net/",
                    LinkedinLink = "https://www.linkedin.com/company/atos/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/atos.net",
                    Description = "A global leader in digital transformation with expertise in cybersecurity, cloud, and high-performance computing.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 17,
                    Name = "Axis",
                    Location = "Cairo",
                    CareersLink = "https://www.axisapp.com/",
                    LinkedinLink = "https://www.linkedin.com/company/axisapp/",
                    IndustryId = 2,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/axisapp.com",
                    Description = "A financial wellness app that provides users with access to their earned wages and financial guidance.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 18,
                    Name = "Banque du Caire",
                    Location = "Cairo, Alexandria",
                    CareersLink = "https://www.bdc.com.eg/bdcwebsite/careers.html",
                    LinkedinLink = "https://www.linkedin.com/company/bdcegypt/",
                    IndustryId = 2,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/bdc.com.eg",
                    Description = "One of Egypt's oldest and largest banks, providing a full range of banking services to corporate and retail customers.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 19,
                    Name = "Banque Misr",
                    Location = "Cairo, Alexandria",
                    CareersLink = "http://www.banquemisr.com",
                    LinkedinLink = "https://www.linkedin.com/company/banque-misr/",
                    IndustryId = 2,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/banquemisr.com",
                    Description = "Established in 1920, Banque Misr is a pioneer in the Egyptian banking sector, offering a wide array of financial products and services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 20,
                    Name = "Beetleware",
                    Location = "Cairo",
                    CareersLink = "https://careers.beetleware.com/",
                    LinkedinLink = "https://www.linkedin.com/company/beetlewaregroup/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/beetleware.com",
                    Description = "A software development and technology services company that helps businesses innovate and grow through custom digital solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 21,
                    Name = "BGP",
                    Location = "Cairo",
                    CareersLink = "https://biogenericpharma.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/biogeneric-pharma/",
                    IndustryId = 3,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/biogenericpharma.com",
                    Description = "A pharmaceutical company focused on producing high-quality, affordable generic medications.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 22,
                    Name = "BlackStone eIT",
                    Location = "Cairo",
                    CareersLink = "https://www.blackstoneeit.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/blackstoneeit/",
                    IndustryId = 1,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/blackstoneeit.com",
                    Description = "A leading global IT service provider, offering innovative technology solutions, consulting, and outsourcing services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                }, 
                new Company
                {
                    CompanyId = 23,
                    Name = "Blink22",
                    Location = "Alexandria",
                    CareersLink = "https://www.blink22.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/blink22/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/blink22.com",
                    Description = "A software development agency that builds high-quality mobile and web applications for startups and established companies.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 24,
                    Name = "blnk",
                    Location = "Giza",
                    CareersLink = "https://blnk.zenats.com/en/careers_page",
                    LinkedinLink = "https://www.linkedin.com/company/blnkconsumerfinance/",
                    IndustryId = 2,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/blnk.ai",
                    Description = "A fintech company enabling instant consumer financing for merchants at the point of sale, driving growth and financial inclusion.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 25,
                    Name = "Bosta",
                    Location = "Cairo",
                    CareersLink = "https://jobs.lever.co/Bosta",
                    LinkedinLink = "https://www.linkedin.com/company/bostaapp/",
                    IndustryId = 8,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/bosta.co",
                    Description = "A tech-enabled logistics company that provides fast and reliable last-mile delivery services for e-commerce businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 26,
                    Name = "Bravo",
                    Location = "Cairo",
                    CareersLink = "-",
                    LinkedinLink = "https://www.linkedin.com/company/bravo-shop-right/",
                    IndustryId = 7,
                    CompanySize = "11-50 employees",
                    LogoUrl = "",
                    Description = "Bravo is an application that allows users to shop from all stores in one place with a unified cart and checkout.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 27,
                    Name = "Breadfast",
                    Location = "Cairo",
                    CareersLink = "https://www.breadfast.com/careers_categories/technology/",
                    LinkedinLink = "https://www.linkedin.com/company/breadfast/",
                    IndustryId = 7,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/breadfast.com",
                    Description = "An online grocery delivery platform that provides scheduled and on-demand delivery of fresh bread, groceries, and household essentials.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 28,
                    Name = "BrightSkies",
                    Location = "Alexandria, Smart Village",
                    CareersLink = "https://brightskiesinc.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/brightskies/",
                    IndustryId = 1,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/brightskiesinc.com",
                    Description = "A technology company delivering high-quality agile software development, with a focus on IoT, cloud solutions, and automotive software.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 29,
                    Name = "Brimore",
                    Location = "Cairo",
                    CareersLink = "http://www.brimore.com",
                    LinkedinLink = "https://www.linkedin.com/company/brimore/",
                    IndustryId = 7,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/brimore.com",
                    Description = "A social commerce platform that allows manufacturers to sell their products directly to consumers through a network of sellers.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 30,
                    Name = "Buseet",
                    Location = "Cairo",
                    CareersLink = "http://www.buseet.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/buseet/",
                    IndustryId = 8,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/buseet.com",
                    Description = "A tech-based transit company offering a comfortable, affordable, and reliable bus network for daily commuters.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 31,
                    Name = "Canal Sugar",
                    Location = "Cairo",
                    CareersLink = "http://www.canalsugar.com",
                    LinkedinLink = "https://www.linkedin.com/company/canal-sugar/",
                    IndustryId = 6,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/canalsugar.com",
                    Description = "A large-scale agricultural and industrial project focused on beet sugar production in Al Minya, Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 32,
                    Name = "CairoMotive",
                    Location = "Cairo",
                    CareersLink = "https://www.cairomotive.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/cairomotive/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/cairomotive.com",
                    Description = "A software development company specializing in providing high-quality custom software solutions and services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 33,
                    Name = "Careem",
                    Location = "Alexandria, Smart Village",
                    CareersLink = "https://www.careem.com/",
                    LinkedinLink = "https://www.linkedin.com/company/careem/",
                    IndustryId = 8,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/careem.com",
                    Description = "The everyday Super App for the greater Middle East, offering services in ride-hailing, food delivery, payments, and more.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 34,
                    Name = "Cartona",
                    Location = "Giza",
                    CareersLink = "http://www.cartona.com/",
                    LinkedinLink = "https://www.linkedin.com/company/cartona-egypt/",
                    IndustryId = 7,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/cartona.com",
                    Description = "A B2B e-commerce platform that connects retailers with wholesalers and manufacturers, digitizing the traditional trade market.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 35,
                    Name = "Cellula Technologies",
                    Location = "Giza",
                    CareersLink = "https://cellula-tech.com/",
                    LinkedinLink = "https://www.linkedin.com/company/cellula-technologies/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/cellula-tech.com",
                    Description = "A software company providing innovative IT solutions and services to help businesses achieve digital transformation.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 36,
                    Name = "Cegedim Egypt",
                    Location = "New Cairo",
                    CareersLink = "https://careers.cegedim.com/en",
                    LinkedinLink = "https://www.linkedin.com/company/cegedim-egypt/",
                    IndustryId = 1,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/cegedim.com",
                    Description = "A global technology and services company specializing in the healthcare field, providing software, data flow management, and BPO services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 37,
                    Name = "CIB Egypt",
                    Location = "Cairo, Alexandria",
                    CareersLink = "https://www.cibeg.com/en/careers",
                    LinkedinLink = "https://www.linkedin.com/company/cibegypt/",
                    IndustryId = 2,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/cibeg.com",
                    Description = "Egypt's leading private-sector bank, offering a broad range of financial products and services to its customers, including enterprises and individuals.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 38,
                    Name = "Concentrix",
                    Location = "Smart Village",
                    CareersLink = "https://jobs.concentrix.com/",
                    LinkedinLink = "https://www.linkedin.com/company/concentrix/",
                    IndustryId = 9,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/concentrix.com",
                    Description = "A leading global provider of customer experience (CX) solutions and technology, improving business performance for the world's best brands.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 39,
                    Name = "Contact",
                    Location = "Cairo",
                    CareersLink = "https://contact.eg/careers",
                    LinkedinLink = "https://www.linkedin.com/company/contact-eg/",
                    IndustryId = 2,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/contact.eg",
                    Description = "A leading provider of non-bank financial services in Egypt, offering a range of financing, insurance, and other financial products.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 40,
                    Name = "Cortex Innovations LLC",
                    Location = "Alexandria",
                    CareersLink = "https://cortex-innovations.com/",
                    LinkedinLink = "https://www.linkedin.com/company/cortex-innovations-llc/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/cortex-innovations.com",
                    Description = "A software development and creative digital marketing company that provides innovative solutions for businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 41,
                    Name = "CrewTeQ",
                    Location = "Cairo",
                    CareersLink = "https://www.crewteq.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/crewteq/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/crewteq.com",
                    Description = "An IT staff augmentation company that builds dedicated remote development teams for global clients from a talent pool in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 42,
                    Name = "CUBE Consultants",
                    Location = "Cairo",
                    CareersLink = "http://www.cubeconsultants.org",
                    LinkedinLink = "https://www.linkedin.com/company/cube-consultants--egypt/",
                    IndustryId = 9,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/cubeconsultants.org",
                    Description = "An architecture, planning, and engineering consultancy firm providing comprehensive design and supervision services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 43,
                    Name = "Cyshield",
                    Location = "Cairo",
                    CareersLink = "https://careers.cyshield.com/",
                    LinkedinLink = "https://www.linkedin.com/company/cyshield/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/cyshield.com",
                    Description = "A global cybersecurity company providing managed detection and response (MDR) services to help organizations combat cyber threats.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 44,
                    Name = "Daily Software Jobs",
                    Location = "Cairo",
                    CareersLink = "https://www.facebook.com/profile.php?id=100086298362453",
                    LinkedinLink = "https://www.linkedin.com/company/daily-software-jobs/",
                    IndustryId = 12,
                    CompanySize = "1-10 employees",
                    LogoUrl = "",
                    Description = "A platform dedicated to posting and sharing daily software job vacancies available in the Egyptian market.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 45,
                    Name = "Dopay",
                    Location = "Cairo",
                    CareersLink = "https://www.dopay.com/",
                    LinkedinLink = "https://www.linkedin.com/company/dopay/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/dopay.com",
                    Description = "A fintech company that provides a platform for employers to pay unbanked workers electronically through a mobile app and debit card.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 46,
                    Name = "Drive Finance",
                    Location = "New cairo",
                    CareersLink = "https://www.drive-finance.com/",
                    LinkedinLink = "https://www.linkedin.com/company/drive-finance-auto/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/drive-finance.com",
                    Description = "A financial services company specializing in auto financing and leasing solutions for consumers in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 47,
                    Name = "Dsquares",
                    Location = "Cairo",
                    CareersLink = "https://www.dsquares.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/dsquares/",
                    IndustryId = 1,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/dsquares.com",
                    Description = "A full-service loyalty and rewards solutions provider, helping businesses retain customers through customized programs.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 48,
                    Name = "EDECS",
                    Location = "Cairo",
                    CareersLink = "https://www.edecs.com/career-vacancies",
                    LinkedinLink = "https://www.linkedin.com/company/edecs/",
                    IndustryId = 5,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/edecs.com",
                    Description = "A leading engineering, procurement, and construction company in Egypt and the Middle East, specialized in infrastructure, marine, and railway projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 49,
                    Name = "EFG Holding",
                    Location = "Cairo",
                    CareersLink = "https://careers.efghldg.com/",
                    LinkedinLink = "https://www.linkedin.com/company/efgholding/",
                    IndustryId = 2,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/efghldg.com",
                    Description = "A trailblazing financial institution with a universal banking platform across frontier and emerging markets.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 50,
                    Name = "EGBANK",
                    Location = "Giza",
                    CareersLink = "https://www.eg-bank.com/En/Careers",
                    LinkedinLink = "https://www.linkedin.com/company/egbankegypt/",
                    IndustryId = 2,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/eg-bank.com",
                    Description = "An Egyptian bank offering innovative banking solutions and services tailored for youth, startups, and businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 51,
                    Name = "Ejad",
                    Location = "Nasr City",
                    CareersLink = "http://www.ejad.com.eg/",
                    LinkedinLink = "https://www.linkedin.com/company/ejad/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/ejad.com.eg",
                    Description = "An IT solutions provider that helps enterprises with digital transformation through software and system integration services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 52,
                    Name = "Ejada",
                    Location = "Cairo",
                    CareersLink = "https://career.ejada.com/",
                    LinkedinLink = "https://www.linkedin.com/company/ejada/about/",
                    IndustryId = 1,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/ejada.com",
                    Description = "A leading IT services & solutions provider in the Middle East & Africa, enabling enterprises to maintain a competitive edge.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 53,
                    Name = "El-Araby",
                    Location = "Cairo",
                    CareersLink = "https://careers.elarabygroup.com/",
                    LinkedinLink = "https://www.linkedin.com/company/elarabygroup/about/",
                    IndustryId = 6,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/elarabygroup.com",
                    Description = "A leading Egyptian enterprise in manufacturing and marketing of electronic and home appliances.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 54,
                    Name = "El Hazek Construction",
                    Location = "Cairo",
                    CareersLink = "https://elhazek.com/en/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/egyptian-union-for-construction-elhazek/",
                    IndustryId = 5,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/elhazek.com",
                    Description = "A leading construction company in Egypt and the region, providing integrated services in engineering, procurement, and construction.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 55,
                    Name = "Elevate Holding",
                    Location = "Cairo",
                    CareersLink = "https://elevateholding.net/",
                    LinkedinLink = "https://www.linkedin.com/company/elevateholding/",
                    IndustryId = 12,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/elevateholding.net",
                    Description = "A holding company that builds and fosters businesses in various sectors, aiming to create value and drive innovation.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 56,
                    Name = "elmenus",
                    Location = "Cairo",
                    CareersLink = "https://elmenus.recruitee.com/",
                    LinkedinLink = "https://www.linkedin.com/company/elmenus.com/",
                    IndustryId = 7,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/elmenus.com",
                    Description = "A comprehensive food discovery and ordering platform in Egypt, helping people decide what to eat and from where.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 57,
                    Name = "ElSewedy Electric",
                    Location = "Cairo",
                    CareersLink = "https://elsewedyelectric.com/en/careers",
                    LinkedinLink = "https://www.linkedin.com/company/elsewedyelectric/",
                    IndustryId = 6,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/elsewedyelectric.com",
                    Description = "A global leader in integrated energy solutions, cables, and electrical products, with a strong presence in Africa and the Middle East.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 58,
                    Name = "Emaar Misr",
                    Location = "Cairo",
                    CareersLink = "http://www.emaarmisr.com",
                    LinkedinLink = "https://www.linkedin.com/company/emaar-misr/",
                    IndustryId = 4,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/emaarmisr.com",
                    Description = "A leading developer of premium lifestyle communities in Egypt, creating world-class integrated developments.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 59,
                    Name = "Embedded Meetup",
                    Location = "Cairo",
                    CareersLink = "https://embeddedmeetup.net/",
                    LinkedinLink = "https://www.linkedin.com/company/embeddedmeetup/",
                    IndustryId = 12,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/embeddedmeetup.net",
                    Description = "A community for embedded systems enthusiasts and professionals in Egypt to connect, learn, and share knowledge.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 60,
                    Name = "Enozom Software",
                    Location = "Alexandria",
                    CareersLink = "https://enozom.com/",
                    LinkedinLink = "https://www.linkedin.com/company/enozom/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/enozom.com",
                    Description = "A software development company providing custom web and mobile application development, as well as software testing services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 61,
                    Name = "Egyptian Projects Operation and Maintenance (EPROM)",
                    Location = "Alexandria",
                    CareersLink = "https://careers.eprom.com.eg/en/Careers/",
                    LinkedinLink = "https://www.linkedin.com/company/eprom/",
                    IndustryId = 6,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/eprom.com.eg",
                    Description = "A leading provider of operation and maintenance management services for the oil, gas, and petrochemical industries.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 62,
                    Name = "EraaSoft",
                    Location = "Cairo",
                    CareersLink = "https://www.eraasoft.com/",
                    LinkedinLink = "https://linkedin.com/company/eraasoft/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/eraasoft.com",
                    Description = "A software company that provides professional training courses in software development and offers web and mobile app development services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 63,
                    Name = "Ericsson",
                    Location = "Smart Village",
                    CareersLink = "https://www.ericsson.com/en/careers",
                    LinkedinLink = "https://www.linkedin.com/company/ericsson/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/ericsson.com",
                    Description = "A global leader in communication technology and services, enabling the full value of connectivity for service providers.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 64,
                    Name = "Espace",
                    Location = "Alexandria",
                    CareersLink = "https://espace.com.eg/jobs/",
                    LinkedinLink = "https://www.linkedin.com/company/espace/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/espace.com.eg",
                    Description = "A software development company providing a wide range of services including web development, mobile apps, and enterprise solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 65,
                    Name = "Etisalat",
                    Location = "Smart Village",
                    CareersLink = "https://careers.etisalat.ae/en/index.html",
                    LinkedinLink = "https://www.linkedin.com/company/eanduae/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/eand.com",
                    Description = "e& (formerly Etisalat Group) is one of the world’s leading technology and investment groups, creating a brighter, digital future.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 66,
                    Name = "EVA pharma",
                    Location = "Giza",
                    CareersLink = "https://www.evapharma.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/eva-pharma/",
                    IndustryId = 3,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/evapharma.com",
                    Description = "One of the fastest-growing pharmaceutical companies in the MENA region, focused on improving patient health and quality of life.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 67,
                    Name = "Eventum Solutions",
                    Location = "Smouha",
                    CareersLink = "https://eventumsolutions.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/eventum-solutions/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/eventumsolutions.com",
                    Description = "A software house that specializes in developing innovative web and mobile applications for various industries.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 68,
                    Name = "ExpandCart",
                    Location = "Cairo",
                    CareersLink = "https://www.expandcart.com",
                    LinkedinLink = "https://www.linkedin.com/company/expandcart/",
                    IndustryId = 7,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/expandcart.com",
                    Description = "A leading e-commerce platform in the MENA region that allows individuals and businesses to create online stores quickly and easily.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 69,
                    Name = "Extreme Solution",
                    Location = "Cairo",
                    CareersLink = "https://extremesolution.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/extreme-solution/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/extremesolution.com",
                    Description = "A software development and technology consulting company that delivers cutting-edge solutions for businesses worldwide.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 70,
                    Name = "Fatura",
                    Location = "Cairo",
                    CareersLink = "http://www.faturab2b.com",
                    LinkedinLink = "https://www.linkedin.com/company/fatura-فاتورة/",
                    IndustryId = 7,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/faturab2b.com",
                    Description = "A B2B marketplace that connects wholesalers with retailers in the FMCG industry, offering a seamless ordering process.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 71,
                    Name = "Fawry",
                    Location = "Cairo",
                    CareersLink = "https://fawry.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/fawry/",
                    IndustryId = 2,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/fawry.com",
                    Description = "The leading Egyptian Digital Transformation & E-Payments Platform, offering financial services to consumers and businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 72,
                    Name = "Fekra Technologies",
                    Location = "Cairo",
                    CareersLink = "https://fekra-egy.com/contact-us/",
                    LinkedinLink = "https://www.linkedin.com/company/fekra-egy/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/fekra-egy.com",
                    Description = "An Odoo Gold Partner in Egypt providing ERP solutions and business management software services to streamline operations.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 73,
                    Name = "Flairs Tech",
                    Location = "Cairo",
                    CareersLink = "https://flairstech.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/flairstech/",
                    IndustryId = 1,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/flairstech.com",
                    Description = "An international IT and Software Services company providing business solutions, software development, and BPO services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 74,
                    Name = "Foodics",
                    Location = "Cairo",
                    CareersLink = "https://www.foodics.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/foodics/",
                    IndustryId = 1,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/foodics.com",
                    Description = "A leading Restaurant-Tech company in MENA that provides an all-in-one POS and restaurant management system.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 75,
                    Name = "Forsa",
                    Location = "New Cairo",
                    CareersLink = "https://www.forsaegypt.com/",
                    LinkedinLink = "https://www.linkedin.com/company/forsaegypt/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/forsaegypt.com",
                    Description = "A Buy-Now-Pay-Later (BNPL) service provider that allows customers to purchase goods and services on installment plans.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 76,
                    Name = "FRONERI",
                    Location = "Cairo",
                    CareersLink = "https://www.froneri.com/",
                    LinkedinLink = "https://www.linkedin.com/company/froneriice-creamegypt/",
                    IndustryId = 6,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/froneri.com",
                    Description = "A global ice cream company, producing iconic brands and creating delicious ice cream for people all over the world.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 77,
                    Name = "Gama Construction",
                    Location = "Cairo",
                    CareersLink = "https://careers.gama.com.eg/",
                    LinkedinLink = "https://www.linkedin.com/company/gamaconstructionegypt/",
                    IndustryId = 5,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/gama.com.eg",
                    Description = "A leading private construction company in Egypt providing integrated engineering, procurement, and construction services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 78,
                    Name = "Garraio, LLC",
                    Location = "Cairo",
                    CareersLink = "https://garraio.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/garraiollc/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/garraio.com",
                    Description = "A technology company specializing in IoT, AI, and custom software solutions to empower businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 79,
                    Name = "Gates Developments",
                    Location = "Giza",
                    CareersLink = "https://gatesdevelopments.com/",
                    LinkedinLink = "https://www.linkedin.com/company/gatesdevelopments/",
                    IndustryId = 4,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/gatesdevelopments.com",
                    Description = "A real estate development company in Egypt, aiming to deliver unique and high-quality residential and commercial projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 80,
                    Name = "geidea",
                    Location = "Cairo",
                    CareersLink = "https://geidea.net",
                    LinkedinLink = "https://www.linkedin.com/company/geidea/",
                    IndustryId = 2,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/geidea.net",
                    Description = "A leading fintech company offering digital payment solutions, POS terminals, and payment gateways for businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 81,
                    Name = "GetPayIn",
                    Location = "Cairo",
                    CareersLink = "https://getpayin.com/",
                    LinkedinLink = "https://www.linkedin.com/company/getpayin/",
                    IndustryId = 2,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/getpayin.com",
                    Description = "A financial technology company that provides online payment gateway solutions for businesses in the MENA region.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 82,
                    Name = "Giza Systems",
                    Location = "Cairo",
                    CareersLink = "https://www.gizasystemscareers.com/en/",
                    LinkedinLink = "https://www.linkedin.com/company/giza-systems/",
                    IndustryId = 1,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/gizasystems.com",
                    Description = "A leading systems integrator in the MEA region, designing and deploying industry-specific technology solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 83,
                    Name = "Global Technologies",
                    Location = "Unknown",
                    CareersLink = "https://www.gt-ict.com/",
                    LinkedinLink = "https://www.linkedin.com/company/gt-global-technologies/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/gt-ict.com",
                    Description = "An ICT solutions provider offering services in infrastructure, managed services, and digital transformation.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 84,
                    Name = "Goodsmart",
                    Location = "Cairo",
                    CareersLink = "http://www.goodsmartegypt.com",
                    LinkedinLink = "https://www.linkedin.com/company/goodsmart-egypt/",
                    IndustryId = 7,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/goodsmartegypt.com",
                    Description = "An online grocery service that provides employees of contracted companies with groceries on a monthly subscription basis.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 85,
                    Name = "Goodzii",
                    Location = "Cairo",
                    CareersLink = "https://www.goodzii.com/",
                    LinkedinLink = "https://www.linkedin.com/company/goodzii/",
                    IndustryId = 7,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/goodzii.com",
                    Description = "An e-commerce platform offering a curated selection of products across various categories, focusing on quality and unique items.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 86,
                    Name = "Halan",
                    Location = "Giza",
                    CareersLink = "https://halan.com/",
                    LinkedinLink = "https://www.linkedin.com/company/halan/",
                    IndustryId = 8,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/mnt-halan.com",
                    Description = "Part of MNT-Halan, Egypt's leading fintech ecosystem, offering services from ride-hailing and delivery to digital payments and loans.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 87,
                    Name = "Hassan Allam Holding",
                    Location = "Cairo",
                    CareersLink = "https://www.hassanallam.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/hassan-allam-holding/",
                    IndustryId = 5,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/hassanallam.com",
                    Description = "One of the largest privately owned corporations in Egypt and the MENA region, specializing in engineering, construction, and infrastructure.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 88,
                    Name = "Henkel",
                    Location = "Cairo",
                    CareersLink = "https://www.henkel.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/henkel/",
                    IndustryId = 6,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/henkel.com",
                    Description = "A global leader in adhesive technologies and consumer brands, including laundry, home care, and beauty products.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 89,
                    Name = "Hewlett Packard Enterprise",
                    Location = "Smart Village",
                    CareersLink = "https://careers.hpe.com/us/en",
                    LinkedinLink = "https://www.linkedin.com/company/hewlett-packard-enterprise/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/hpe.com",
                    Description = "The global edge-to-cloud company built to transform businesses by helping them connect, protect, analyze, and act on all their data.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 90,
                    Name = "Hikma Pharmaceuticals",
                    Location = "Cairo",
                    CareersLink = "https://www.hikma.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/hikma-pharmaceuticals/",
                    IndustryId = 3,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/hikma.com",
                    Description = "A global pharmaceutical company focused on developing, manufacturing and marketing a broad range of branded and non-branded generic medicines.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 91,
                    Name = "Homzmart",
                    Location = "Cairo",
                    CareersLink = "https://careers.smartrecruiters.com/Homzmart",
                    LinkedinLink = "https://www.linkedin.com/company/homzmart/",
                    IndustryId = 7,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/homzmart.com",
                    Description = "A leading e-commerce marketplace for furniture and home goods, connecting consumers with a wide range of manufacturers and brands.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 92,
                    Name = "Huawei",
                    Location = "Smart Village",
                    CareersLink = "https://career.huawei.com/reccampportal/portal5/index.html",
                    LinkedinLink = "https://www.linkedin.com/company/huawei/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/huawei.com",
                    Description = "A leading global provider of information and communications technology (ICT) infrastructure and smart devices.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 93,
                    Name = "Hyde Park Developments",
                    Location = "Cairo",
                    CareersLink = "https://careers.hpd.com.eg/",
                    LinkedinLink = "https://www.linkedin.com/company/hydepark-developments/",
                    IndustryId = 4,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/hpd.com.eg",
                    Description = "A prominent real estate developer in Egypt, renowned for creating large-scale, high-quality integrated communities.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 94,
                    Name = "IBM",
                    Location = "Smart Village",
                    CareersLink = "https://www.ibm.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/ibm/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/ibm.com",
                    Description = "A global technology company that provides hardware, software, cloud-based services, and cognitive computing.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 95,
                    Name = "Implicit",
                    Location = "Cairo",
                    CareersLink = "https://www.implicit.cloud/",
                    LinkedinLink = "https://www.linkedin.com/company/implicit-cloud/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/implicit.cloud",
                    Description = "A cloud-native services company that helps businesses design, build, and manage their cloud infrastructure and applications.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 96,
                    Name = "Incorta",
                    Location = "Sidi Gaber & New Cairo",
                    CareersLink = "https://www.incorta.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/incorta/",
                    IndustryId = 1,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/incorta.com",
                    Description = "An all-in-one data and analytics platform that unifies complex data sources and provides a single source of truth for business insights.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 97,
                    Name = "Inova",
                    Location = "Alexandria",
                    CareersLink = "https://inovaeg.com/jobs/",
                    LinkedinLink = "https://www.linkedin.com/company/inovaeg/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/inovaeg.com",
                    Description = "A software solutions provider specializing in GIS, enterprise resource planning, and custom application development.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 98,
                    Name = "InstaBug",
                    Location = "Cairo",
                    CareersLink = "https://www.instabug.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/instabug/",
                    IndustryId = 1,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/instabug.com",
                    Description = "A platform providing real-time contextual insights for mobile apps through bug reporting, crash reporting, and user feedback.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 99,
                    Name = "instashop",
                    Location = "Cairo",
                    CareersLink = "https://instashop.com/en-ae/careers",
                    LinkedinLink = "https://www.linkedin.com/company/instashop-convenience-delivered/",
                    IndustryId = 7,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/instashop.com",
                    Description = "An online grocery delivery service that allows users to order from nearby supermarkets and get their items delivered quickly.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 100,
                    Name = "ischool",
                    Location = "New Cairo",
                    CareersLink = "https://www.ischooltech.com/eg/home-ar",
                    LinkedinLink = "https://www.linkedin.com/company/ischooltech/",
                    IndustryId = 10,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/ischooltech.com",
                    Description = "An educational technology company offering accredited STEM learning programs for kids and teens in fields like programming and AI.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 101,
                    Name = "Isekai Code",
                    Location = "Maadi",
                    CareersLink = "https://www.isekai-code.com/",
                    LinkedinLink = "https://www.linkedin.com/company/isekai-code/",
                    IndustryId = 1,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/isekai-code.com",
                    Description = "A software development company focused on building innovative and high-quality web and mobile applications for clients.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 102,
                    Name = "ITI",
                    Location = "Smart Village",
                    CareersLink = "https://iti.gov.eg/home",
                    LinkedinLink = "https://www.linkedin.com/school/information-technology-institute-iti-/",
                    IndustryId = 10,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/iti.gov.eg",
                    Description = "Egypt's national Information Technology Institute, which provides professional development and training in ICT fields.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 103,
                    Name = "Itida",
                    Location = "Smart Village",
                    CareersLink = "https://itida.gov.eg/English/Careers/Pages/default.aspx",
                    LinkedinLink = "https://www.linkedin.com/company/itida/",
                    IndustryId = 11,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/itida.gov.eg",
                    Description = "The Information Technology Industry Development Agency, a governmental entity responsible for growing and developing the IT industry in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 104,
                    Name = "ITWorx",
                    Location = "Cairo",
                    CareersLink = "https://www.itworx.com/jobs/",
                    LinkedinLink = "https://www.linkedin.com/company/itworx/",
                    IndustryId = 1,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/itworx.com",
                    Description = "A global software professional services company that helps businesses with their digital transformation through innovative solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 105,
                    Name = "IWAN Developments",
                    Location = "Cairo",
                    CareersLink = "https://iwandevelopments.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/iwan-developments/",
                    IndustryId = 4,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/iwandevelopments.com",
                    Description = "A real estate development company in Egypt focused on creating unique, high-end residential communities.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 106,
                    Name = "J&T Express Egypt",
                    Location = "Cairo",
                    CareersLink = "https://www.jtexpress-eg.com/joinus",
                    LinkedinLink = "https://www.linkedin.com/company/jtexpress-eg/",
                    IndustryId = 8,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/jtexpress-eg.com",
                    Description = "A global logistics and express delivery company providing fast, reliable, and technology-based shipping services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 107,
                    Name = "Jumia",
                    Location = "New Cairo",
                    CareersLink = "https://group.jumia.com/careers?location=egypt",
                    LinkedinLink = "https://www.linkedin.com/company/jumia-egypt/",
                    IndustryId = 7,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/jumia.com.eg",
                    Description = "A leading Pan-African e-commerce platform, connecting millions of consumers and sellers through its marketplace, logistics, and payment services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 108,
                    Name = "Kader & Associates Designs",
                    Location = "Cairo",
                    CareersLink = "https://kadesigns-eg.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/ka-designs/",
                    IndustryId = 9,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/kadesigns-eg.com",
                    Description = "An architectural and engineering consulting firm that offers integrated design services for a wide range of projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 109,
                    Name = "KarmSolar",
                    Location = "Cairo",
                    CareersLink = "https://www.karmsolar.com/",
                    LinkedinLink = "https://www.linkedin.com/company/karmsolar/",
                    IndustryId = 6,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/karmsolar.com",
                    Description = "A solar technology and integration company that delivers innovative solar solutions to the industrial, commercial, and agricultural sectors in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 110,
                    Name = "KAYAN Group",
                    Location = "Alexandria, Cairo",
                    CareersLink = "https://www.careers-page.com/kayan-group",
                    LinkedinLink = "https://www.linkedin.com/company/kayan-group-automotive/",
                    IndustryId = 7,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/kayan-egypt.com",
                    Description = "A leading automotive group in Egypt, the sole importer of several international car brands including SEAT, SKODA, and Volkswagen.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 111,
                    Name = "Kazyon",
                    Location = "Cairo",
                    CareersLink = "https://store.kazyonplus.com/store/kazyon/en/joinus",
                    LinkedinLink = "https://www.linkedin.com/company/kazyon/",
                    IndustryId = 7,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/kazyonplus.com",
                    Description = "The largest hard-discounter retail chain in Egypt, offering basic consumer goods at affordable prices through a network of stores.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 112,
                    Name = "KB Architects",
                    Location = "Cairo",
                    CareersLink = "https://www.kbarchitects.org/",
                    LinkedinLink = "https://www.linkedin.com/company/kbarch/",
                    IndustryId = 9,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/kbarchitects.org",
                    Description = "An architectural firm providing design, planning, and consultancy services for a variety of building projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 113,
                    Name = "Khazenly",
                    Location = "Cairo",
                    CareersLink = "https://khazenly.com/en/jobs/",
                    LinkedinLink = "https://www.linkedin.com/company/khazenly/",
                    IndustryId = 8,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/khazenly.com",
                    Description = "A tech-enabled logistics platform offering on-demand warehousing and fulfillment solutions for businesses of all sizes.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 114,
                    Name = "Khazna",
                    Location = "Cairo",
                    CareersLink = "http://www.khazna.app",
                    LinkedinLink = "https://www.linkedin.com/company/khazna/",
                    IndustryId = 2,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/khazna.app",
                    Description = "A financial super app that provides digital financial services, including salary advance, bill payments, and a savings feature.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 115,
                    Name = "KINNOVIA",
                    Location = "Cairo",
                    CareersLink = "https://kinnovia.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/kinnovia-gmbh/about/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/kinnovia.com",
                    Description = "A German-Egyptian software company that provides high-quality, custom software solutions and builds dedicated development teams.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 116,
                    Name = "Klivvr",
                    Location = "Cairo",
                    CareersLink = "http://www.klivvr.com",
                    LinkedinLink = "https://www.linkedin.com/company/klivvr/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/klivvr.com",
                    Description = "A fintech company offering a smart payment app and card designed to provide a seamless and secure financial experience for the youth.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 117,
                    Name = "Kredit",
                    Location = "Cairo",
                    CareersLink = "https://www.kredit.com.eg/en/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/kredit-for-smes-finance/",
                    IndustryId = 2,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/kredit.com.eg",
                    Description = "A financial services company that provides a range of financing solutions and support for Small and Medium Enterprises (SMEs) in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 118,
                    Name = "Ldp+Partners",
                    Location = "Cairo",
                    CareersLink = "https://ldppartners.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/ldp-partners/",
                    IndustryId = 9,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/ldppartners.com",
                    Description = "An international design and consulting firm specializing in architecture, planning, engineering, and landscape design.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 119,
                    Name = "Linah Farms",
                    Location = "Giza",
                    CareersLink = "https://linahfarms.com/pages/careers",
                    LinkedinLink = "https://www.linkedin.com/company/linahfarms/",
                    IndustryId = 6,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/linahfarms.com",
                    Description = "A producer of premium quality, natural, and healthy food products, specializing in dairy, beverages, and other farm-fresh goods.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 120,
                    Name = "Link Development",
                    Location = "Cairo",
                    CareersLink = "https://linkdevelopment.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/link-development/",
                    IndustryId = 1,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/linkdevelopment.com",
                    Description = "A global technology solutions provider delivering innovative software, mobile development, and AI solutions to businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 121,
                    Name = "Livit Studio",
                    Location = "Giza",
                    CareersLink = "https://livitstudio.io/",
                    LinkedinLink = "https://www.linkedin.com/showcase/livit-studio/",
                    IndustryId = 9,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/livitstudio.io",
                    Description = "An international design studio specializing in architecture, interior design, and branding for hospitality and F&B concepts.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 122,
                    Name = "Luxoft Egypt",
                    Location = "New Cairo",
                    CareersLink = "https://career.luxoft.com/jobs",
                    LinkedinLink = "https://www.linkedin.com/company/luxoft-egypt/",
                    IndustryId = 1,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/luxoft.com",
                    Description = "A DXC Technology Company and a global digital strategy and software engineering firm providing bespoke technology solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 123,
                    Name = "M squared",
                    Location = "Giza",
                    CareersLink = "http://www.msquaredev.com",
                    LinkedinLink = "https://www.linkedin.com/company/msquaredev/",
                    IndustryId = 4,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/msquaredev.com",
                    Description = "A real estate development company focused on creating innovative and sustainable communities in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 124,
                    Name = "Madinet Masr",
                    Location = "Cairo",
                    CareersLink = "https://www.madinetmasr.com/en/careers",
                    LinkedinLink = "https://www.linkedin.com/company/madinetmasr/",
                    IndustryId = 4,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/madinetmasr.com",
                    Description = "A leading urban communities developer in Egypt with a long history of creating large-scale, integrated real estate projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 125,
                    Name = "Marakez",
                    Location = "Cairo",
                    CareersLink = "https://careers.marakez.net/",
                    LinkedinLink = "https://www.linkedin.com/company/marakez-careers/",
                    IndustryId = 4,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/marakez.net",
                    Description = "A leading Egyptian real estate developer with a focus on creating mixed-use properties and vibrant commercial centers.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 126,
                    Name = "MaxAB",
                    Location = "Cairo",
                    CareersLink = "https://www.maxab.io/#careers",
                    LinkedinLink = "https://www.linkedin.com/company/maxab/",
                    IndustryId = 7,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/maxab.io",
                    Description = "A B2B e-commerce platform that connects informal food and grocery retailers with suppliers through a user-friendly app.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 127,
                    Name = "McKinsey & Company",
                    Location = "New Cairo",
                    CareersLink = "https://www.mckinsey.com/middle-east/careers/careers-in-the-middle-east",
                    LinkedinLink = "https://www.linkedin.com/company/mckinsey/",
                    IndustryId = 9,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/mckinsey.com",
                    Description = "A global management consulting firm that serves leading businesses, governments, and organizations to achieve lasting improvements.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 128,
                    Name = "MCS",
                    Location = "Cairo",
                    CareersLink = "https://www.mcsoil.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/marine-construction-services/",
                    IndustryId = 5,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/mcsoil.com",
                    Description = "An engineering services company specializing in marine construction, subsea services, and solutions for the oil and gas industry.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 129,
                    Name = "MENA ALLIANCES",
                    Location = "Alexandria",
                    CareersLink = "https://menaalliances.com/",
                    LinkedinLink = "https://www.linkedin.com/company/menaalliances/",
                    IndustryId = 9,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/menaalliances.com",
                    Description = "A consulting firm that provides business development, strategic partnerships, and market entry services in the MENA region.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 130,
                    Name = "Microsoft",
                    Location = "Smart Village",
                    CareersLink = "https://careers.microsoft.com/v2/global/en/home.html",
                    LinkedinLink = "https://www.linkedin.com/company/microsoft/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/microsoft.com",
                    Description = "A global technology leader that enables digital transformation for the era of an intelligent cloud and an intelligent edge.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 131,
                    Name = "Misr Italia Properties",
                    Location = "Cairo",
                    CareersLink = "https://www.misritaliaproperties.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/misritaliaproperties/",
                    IndustryId = 4,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/misritaliaproperties.com",
                    Description = "A leading real estate developer in Egypt, known for creating iconic residential, commercial, and hospitality projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 132,
                    Name = "MNT-Halan",
                    Location = "Cairo",
                    CareersLink = "https://mnt-halan.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/mnt-halan/",
                    IndustryId = 2,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/mnt-halan.com",
                    Description = "Egypt's leading fintech ecosystem and the largest non-bank lender to the unbanked and underbanked.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 133,
                    Name = "Modeso",
                    Location = "Alexandria, Cairo",
                    CareersLink = "https://www.modeso.ch/careers",
                    LinkedinLink = "https://www.linkedin.com/company/modeso/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/modeso.ch",
                    Description = "A Swiss software company that designs and develops custom web and mobile applications with development centers in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 134,
                    Name = "Mogo",
                    Location = "Giza",
                    CareersLink = "https://mogo-eg.com/",
                    LinkedinLink = "https://www.linkedin.com/company/mogo-eg/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/mogo-eg.com",
                    Description = "A fintech company providing affordable financing solutions for used cars and other assets in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 135,
                    Name = "Money Fellows",
                    Location = "Cairo",
                    CareersLink = "https://www.moneyfellows.com/en-us/careers-page/",
                    LinkedinLink = "https://www.linkedin.com/company/moneyfellows/",
                    IndustryId = 2,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/moneyfellows.com",
                    Description = "A fintech platform that digitizes and scales traditional money circles (ROSCAs or Gam'eya) for users.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 136,
                    Name = "Mountain View",
                    Location = "Cairo",
                    CareersLink = "https://www.mountainviewegypt.com/career",
                    LinkedinLink = "https://www.linkedin.com/company/mountainvieweg/",
                    IndustryId = 4,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/mountainviewegypt.com",
                    Description = "A leading Egyptian real estate development company specializing in high-end residential communities and resorts.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 137,
                    Name = "Mozare3",
                    Location = "6 of October, Giza",
                    CareersLink = "https://mozare3.net/",
                    LinkedinLink = "https://www.linkedin.com/company/mozare3/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/mozare3.net",
                    Description = "An agri-fintech platform that provides smallholder farmers with access to financing, markets, and agronomy support.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 138,
                    Name = "mylerz Co",
                    Location = "Cairo",
                    CareersLink = "https://www.mylerz.com/career",
                    LinkedinLink = "https://www.linkedin.com/company/mylerz-co/",
                    IndustryId = 8,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/mylerz.com",
                    Description = "A tech-driven logistics and fulfillment company providing innovative last-mile delivery and e-commerce solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 139,
                    Name = "Nagarro",
                    Location = "New Cairo",
                    CareersLink = "https://www.nagarro.com",
                    LinkedinLink = "https://www.linkedin.com/company/nagarro/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/nagarro.com",
                    Description = "A global digital engineering leader, helping clients become innovative, digital-first companies and thus win in their markets.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 140,
                    Name = "Nana",
                    Location = "Cairo",
                    CareersLink = "https://hiring.nana.co/",
                    LinkedinLink = "https://www.linkedin.com/company/nana-app/",
                    IndustryId = 7,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/nana.sa",
                    Description = "A leading online grocery delivery platform in the MENA region that connects customers with their favorite supermarkets.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 141,
                    Name = "Naqla",
                    Location = "Cairo",
                    CareersLink = "https://naqla.xyz/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/naqla/",
                    IndustryId = 8,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/naqla.xyz",
                    Description = "A technology platform that connects truck drivers with cargo owners, revolutionizing road freight and logistics in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 142,
                    Name = "National Bank of Egypt (NBE)",
                    Location = "Cairo",
                    CareersLink = "http://www.nbe.com.eg",
                    LinkedinLink = "https://www.linkedin.com/company/national-bank-of-egypt/",
                    IndustryId = 2,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/nbe.com.eg",
                    Description = "The oldest and largest commercial bank in Egypt, providing a comprehensive range of corporate and retail banking services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 143,
                    Name = "Nawah Scientific",
                    Location = "Cairo",
                    CareersLink = "https://nawah-scientific.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/nawah-scientific/",
                    IndustryId = 3,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/nawah-scientific.com",
                    Description = "An online platform offering on-demand scientific and analytical services for researchers and industrial clients in the MENA region.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 144,
                    Name = "Nawy",
                    Location = "New Cairo",
                    CareersLink = "https://apply.workable.com/nawy-real-estate/",
                    LinkedinLink = "https://www.linkedin.com/company/nawyestate/",
                    IndustryId = 4,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/nawy.com",
                    Description = "A property technology company and real estate brokerage that helps clients find their dream homes through a smart, data-driven platform.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 145,
                    Name = "Nexta",
                    Location = "Cairo",
                    CareersLink = "http://getnexta.com",
                    LinkedinLink = "https://www.linkedin.com/company/nextaegypt/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/getnexta.com",
                    Description = "A fintech company providing a next-generation banking app and payment card to help users manage their money effortlessly.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 146,
                    Name = "Nile Developments",
                    Location = "Cairo",
                    CareersLink = "https://nile-developments.com/en/career/",
                    LinkedinLink = "https://www.linkedin.com/company/nile-developments/",
                    IndustryId = 4,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/nile-developments.com",
                    Description = "A real estate development company specializing in creating iconic, high-rise buildings and luxury projects in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 147,
                    Name = "NoGood",
                    Location = "Cairo",
                    CareersLink = "https://nogood.io",
                    LinkedinLink = "https://www.linkedin.com/company/nogood/",
                    IndustryId = 9,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/nogood.io",
                    Description = "A growth marketing agency that partners with brands to rapidly scale their growth through a data-driven, experimental approach.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 148,
                    Name = "Noon",
                    Location = "Cairo",
                    CareersLink = "https://www.noon.com/",
                    LinkedinLink = "https://www.linkedin.com/company/nooncom/",
                    IndustryId = 7,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/noon.com",
                    Description = "A leading digital ecosystem of products and services, born in the Middle East, offering a premier e-commerce and delivery platform.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 149,
                    Name = "Nowlun.com",
                    Location = "Alexandria",
                    CareersLink = "https://apply.workable.com/nowlun/",
                    LinkedinLink = "https://www.linkedin.com/company/nowlun-com/",
                    IndustryId = 8,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/nowlun.com",
                    Description = "A digital freight marketplace that simplifies sea shipping by connecting shippers and carriers on one platform.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 150,
                    Name = "NTI",
                    Location = "Smart Village",
                    CareersLink = "https://www.nti.sci.eg/eta/",
                    LinkedinLink = "https://www.linkedin.com/school/nti-eg/",
                    IndustryId = 10,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/nti.sci.eg",
                    Description = "The National Telecommunication Institute of Egypt, a leading center for training, education, and research in telecommunications.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 151,
                    Name = "NTRA",
                    Location = "Smart Village",
                    CareersLink = "https://www.tra.gov.eg/en/",
                    LinkedinLink = "https://www.linkedin.com/company/ntraeg/",
                    IndustryId = 11,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/tra.gov.eg",
                    Description = "The National Telecom Regulatory Authority of Egypt, responsible for regulating the telecommunications sector and ensuring a competitive market.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 152,
                    Name = "Objects",
                    Location = "Alexandria",
                    CareersLink = "https://objects.ws/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/objects/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/objects.ws",
                    Description = "A software development and IT consulting company that provides innovative solutions and builds dedicated teams for clients.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 153,
                    Name = "ODC",
                    Location = "Cairo",
                    CareersLink = "https://www.orangedigitalcenters.com/country/EG/home",
                    LinkedinLink = "https://www.linkedin.com/company/orange-digital-center-egypt/",
                    IndustryId = 10,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/orangedigitalcenters.com",
                    Description = "Orange Digital Center Egypt is a hub for free digital training, incubation, and acceleration for startups, and support for project owners.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 154,
                    Name = "oliv",
                    Location = "Cairo",
                    CareersLink = "https://oliv.finance/",
                    LinkedinLink = "https://www.linkedin.com/company/oliv-finance/",
                    IndustryId = 2,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/oliv.finance",
                    Description = "A fintech company providing Sharia-compliant financial solutions, including Buy-Now-Pay-Later services, for consumers and businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 155,
                    Name = "oneCommerce",
                    Location = "Cairo",
                    CareersLink = "https://www.onecommerce.group/",
                    LinkedinLink = "https://www.linkedin.com/company/onecommerce-group/",
                    IndustryId = 7,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/onecommerce.group",
                    Description = "An e-commerce services company that builds and scales direct-to-consumer brands in the Middle East.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 156,
                    Name = "Oracle",
                    Location = "Cairo",
                    CareersLink = "https://careers.oracle.com/jobs/#en/sites/jobsearch",
                    LinkedinLink = "https://www.linkedin.com/company/oracle/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/oracle.com",
                    Description = "A global technology corporation that provides a wide range of cloud-based applications, platforms, and computing infrastructure.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 157,
                    Name = "Orascom Construction PLC",
                    Location = "Cairo",
                    CareersLink = "https://careers.orascom.com/",
                    LinkedinLink = "https://www.linkedin.com/company/orascom-construction-plc/",
                    IndustryId = 5,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/orascom.com",
                    Description = "A leading global engineering and construction contractor with a footprint covering the Middle East, Africa, and the United States.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 158,
                    Name = "Orascom Development",
                    Location = "Cairo",
                    CareersLink = "https://www.orascomdh.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/orascom-development/",
                    IndustryId = 4,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/orascomdh.com",
                    Description = "A leading developer of fully integrated destinations, including hotels, private villas, and apartments, with a strong focus on tourism.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 159,
                    Name = "P&G (Procter & Gamble)",
                    Location = "New Cairo",
                    CareersLink = "https://www.pgcareers.com/mea/en",
                    LinkedinLink = "https://www.linkedin.com/company/procter-and-gamble/",
                    IndustryId = 6,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/pg.com",
                    Description = "A global consumer goods company with a portfolio of trusted, quality, leadership brands including Pampers, Tide, and Gillette.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 160,
                    Name = "Palm Hills Developments",
                    Location = "Cairo",
                    CareersLink = "https://careers.palmhillsdevelopments.com/",
                    LinkedinLink = "https://www.linkedin.com/company/palm-hills-developments/",
                    IndustryId = 4,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/palmhillsdevelopments.com",
                    Description = "A leading real estate developer in Egypt, creating integrated residential, commercial, and resort communities.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 161,
                    Name = "Parkville",
                    Location = "Giza",
                    CareersLink = "https://www.parkvillepharma.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/parkville-pharmaceuticals-company/",
                    IndustryId = 3,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/parkvillepharma.com",
                    Description = "A pharmaceutical company dedicated to developing and providing high-quality cosmeceutical and healthcare products.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 162,
                    Name = "Paymob",
                    Location = "Cairo",
                    CareersLink = "https://www.paymob.com/en/careers",
                    LinkedinLink = "https://www.linkedin.com/company/paymobcompany/",
                    IndustryId = 2,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/paymob.com",
                    Description = "A leading financial services enabler in MENAP, providing a comprehensive payment ecosystem for businesses to accept and manage payments.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 163,
                    Name = "Pharco Corporation",
                    Location = "Alexandria",
                    CareersLink = "https://www.pharco.org/careers.aspx",
                    LinkedinLink = "https://www.linkedin.com/company/pharco-corporation/",
                    IndustryId = 3,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/pharco.org",
                    Description = "The largest pharmaceutical manufacturer in the Middle East and Africa, focused on research, development, and production of medicines.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 164,
                    Name = "Pharos Solutions",
                    Location = "Alexandria",
                    CareersLink = "https://www.pharos-solutions.de/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/pharos-solutions-ug/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/pharos-solutions.de",
                    Description = "A German-based IT services and consulting company with a branch in Egypt, specializing in software development and staff augmentation.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 165,
                    Name = "PRE Developments",
                    Location = "Cairo",
                    CareersLink = "https://predevelopments.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/predevelopments/",
                    IndustryId = 4,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/predevelopments.com",
                    Description = "A leading real estate developer in Egypt, committed to delivering innovative and quality-driven residential and mixed-use projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 166,
                    Name = "Premium Card",
                    Location = "Cairo",
                    CareersLink = "https://premiumcard.net/careers",
                    LinkedinLink = "https://www.linkedin.com/company/premium-card/",
                    IndustryId = 2,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/premiumcard.net",
                    Description = "A financial services company offering a credit card that allows users to pay for purchases in interest-free installments.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 167,
                    Name = "Prime Holding",
                    Location = "Giza",
                    CareersLink = "http://www.primeholdingco.com",
                    LinkedinLink = "https://www.linkedin.com/company/prime-holding/",
                    IndustryId = 2,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/primeholdingco.com",
                    Description = "A leading investment banking and financial services company in the MENA region, offering brokerage, asset management, and advisory services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 168,
                    Name = "Procore",
                    Location = "Cairo",
                    CareersLink = "https://careers.procore.com/",
                    LinkedinLink = "https://www.linkedin.com/company/procore-technologies/",
                    IndustryId = 1,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/procore.com",
                    Description = "A leading provider of cloud-based construction management software, connecting project teams from the office to the field.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 169,
                    Name = "ProCrew",
                    Location = "Alexandria",
                    CareersLink = "https://www.procrew.pro/",
                    LinkedinLink = "https://www.linkedin.com/company/procrewpro/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/procrew.pro",
                    Description = "An IT staff augmentation company that helps businesses hire dedicated remote software developers and build professional teams.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 170,
                    Name = "PROMETEON TYRE GROUP",
                    Location = "Alexandria",
                    CareersLink = "https://www.prometeon.com/EG/ar_EG",
                    LinkedinLink = "https://www.linkedin.com/company/prometeontyregroup/",
                    IndustryId = 6,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/prometeon.com",
                    Description = "A global company solely focused on the industrial tyre business for trucks, buses, and agro applications, with manufacturing facilities in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 171,
                    Name = "PWC",
                    Location = "Smouha & New Cairo",
                    CareersLink = "https://www.pwc.com/us/en/careers.html",
                    LinkedinLink = "https://www.linkedin.com/company/pwc-middle-east/",
                    IndustryId = 9,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/pwc.com",
                    Description = "A global network of firms delivering assurance, tax, and advisory services to build trust in society and solve important problems.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 172,
                    Name = "Qalaa Holdings",
                    Location = "Cairo",
                    CareersLink = "https://www.qalaaholdings.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/qalaa-holdings/",
                    IndustryId = 12,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/qalaaholdings.com",
                    Description = "An African leader in energy and infrastructure, building and investing in vital projects across strategic sectors.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 173,
                    Name = "QNB",
                    Location = "Cairo, Giza",
                    CareersLink = "https://www.qnb.com.eg/sites/qnb/qnbegypt/page/en/encareersegypt.html",
                    LinkedinLink = "https://www.linkedin.com/company/qnbeg/",
                    IndustryId = 2,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/qnb.com.eg",
                    Description = "QNB ALAHLI is one of the leading financial institutions in Egypt, providing a wide range of banking services to individuals and corporations.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 174,
                    Name = "Rabbit",
                    Location = "Maadi, Cairo",
                    CareersLink = "https://www.rabbitmart.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/rabbitmart/",
                    IndustryId = 7,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/rabbitmart.com",
                    Description = "An ultra-fast grocery delivery company that promises to deliver groceries and essentials to your doorstep in minutes.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 175,
                    Name = "Rabbit Technology",
                    Location = "Alexandria",
                    CareersLink = "https://rabbittec.com/",
                    LinkedinLink = "https://www.linkedin.com/company/rabbit-technology/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/rabbittec.com",
                    Description = "A software house based in Alexandria that specializes in building custom web and mobile applications for clients.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 176,
                    Name = "Raisa Energy",
                    Location = "Maadi",
                    CareersLink = "https://www.raisa.com/",
                    LinkedinLink = "https://www.linkedin.com/company/raisa-energy/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/raisa.com",
                    Description = "An investment company that acquires and manages oil and gas assets, with a significant technical and analytical presence in Cairo.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 177,
                    Name = "Raya",
                    Location = "6th October City",
                    CareersLink = "https://careers.rayacorp.com/",
                    LinkedinLink = "https://www.linkedin.com/company/raya/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/rayacorp.com",
                    Description = "A leading investment conglomerate managing a diversified portfolio in IT, contact centers, smart buildings, and financial services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 178,
                    Name = "Robusta Studio",
                    Location = "New Cairo, Cairo",
                    CareersLink = "https://robustagroup.com/join-us/",
                    LinkedinLink = "https://www.linkedin.com/company/robusta-studio/",
                    IndustryId = 1,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/robustagroup.com",
                    Description = "A digital product agency and technology consultancy that partners with businesses to design, build, and scale exceptional digital experiences.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 179,
                    Name = "Rowad Modern Engineering",
                    Location = "Cairo",
                    CareersLink = "https://rowad-rme.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/rowad-modern-engineering/",
                    IndustryId = 5,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/rowad-rme.com",
                    Description = "A leading construction company in Egypt that provides integrated engineering, procurement, and construction services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 180,
                    Name = "RSA Security",
                    Location = "New Cairo",
                    CareersLink = "https://www.rsa.com/rsa-careers/",
                    LinkedinLink = "https://www.linkedin.com/company/rsasecurity/",
                    IndustryId = 1,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/rsa.com",
                    Description = "A global leader in cybersecurity and risk management, providing solutions for identity and access management and threat detection.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 181,
                    Name = "Rubikal",
                    Location = "Alexandria",
                    CareersLink = "https://www.rubikal.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/rubikal_llc/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/rubikal.com",
                    Description = "A software development firm that specializes in building scalable web and mobile applications for startups and enterprises.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 182,
                    Name = "Sahl",
                    Location = "Giza",
                    CareersLink = "https://sahl.recruitee.com/#",
                    LinkedinLink = "https://www.linkedin.com/company/sahlpayapp/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/sahl.money",
                    Description = "A fintech application that provides a one-stop solution for paying all household bills and services electronically.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 183,
                    Name = "saib",
                    Location = "Giza",
                    CareersLink = "https://www.saib.com.eg/en/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/saib-bank/",
                    IndustryId = 2,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/saib.com.eg",
                    Description = "An Egyptian bank that provides a full range of retail, corporate, and investment banking services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 184,
                    Name = "Sana Soft",
                    Location = "Alexandria, Cairo",
                    CareersLink = "https://sanasofteg.com/",
                    LinkedinLink = "https://www.linkedin.com/company/sanasoftltd/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/sanasofteg.com",
                    Description = "A software development company that offers web and mobile app development, UI/UX design, and IT consulting services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 185,
                    Name = "Sandoz Egypt",
                    Location = "Cairo",
                    CareersLink = "https://www.sandoz.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/sandozegypt/",
                    IndustryId = 3,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/sandoz.com",
                    Description = "A global leader in generic pharmaceuticals and biosimilars, committed to increasing access to high-quality, life-enhancing medicines.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 186,
                    Name = "SEITech Solutions",
                    Location = "Giza",
                    CareersLink = "http://seitech-solutions.com/",
                    LinkedinLink = "https://www.linkedin.com/company/seitech-solutions-eg/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/seitech-solutions.com",
                    Description = "An IT company providing a range of services including software development, systems integration, and technical support.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 187,
                    Name = "seven",
                    Location = "Giza",
                    CareersLink = "http://www.bseven.com",
                    LinkedinLink = "https://www.linkedin.com/company/bsevenegypt/",
                    IndustryId = 7,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/bseven.com",
                    Description = "A retail company specializing in the sale of consumer electronics, home appliances, and personal care products.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 188,
                    Name = "Shemsi",
                    Location = "Cairo",
                    CareersLink = "https://myshemsi.com/en-EG/",
                    LinkedinLink = "https://www.linkedin.com/company/shemsi/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/myshemsi.com",
                    Description = "A technology platform for solar energy, allowing users to find, purchase, and manage solar solutions for their homes and businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 189,
                    Name = "ShipBlu",
                    Location = "Cairo",
                    CareersLink = "http://shipblu.com/en/",
                    LinkedinLink = "https://www.linkedin.com/company/shipblu/",
                    IndustryId = 8,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/shipblu.com",
                    Description = "A tech-enabled logistics company using AI and machine learning to provide a premium last-mile delivery experience.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 190,
                    Name = "SIAC Construction",
                    Location = "Cairo",
                    CareersLink = "https://www.siac.com.eg/?q=careers",
                    LinkedinLink = "https://www.linkedin.com/company/siac-construction/",
                    IndustryId = 5,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/siac.com.eg",
                    Description = "A leading regional construction company in Egypt, providing integrated engineering, procurement, and construction services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 191,
                    Name = "Siemens",
                    Location = "New Cairo",
                    CareersLink = "https://www.siemens.com/global/en/company/jobs.html",
                    LinkedinLink = "https://www.linkedin.com/company/siemens/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/siemens.com",
                    Description = "A global technology powerhouse that focuses on intelligent infrastructure for buildings and distributed energy systems.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 192,
                    Name = "Silicon Mind",
                    Location = "Alexandria",
                    CareersLink = "https://silicon-mind.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/siliconmind/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/silicon-mind.com",
                    Description = "A software development company offering services in web development, mobile applications, and enterprise solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 193,
                    Name = "Smartera 3S Solutions and Systems",
                    Location = "Alexandria",
                    CareersLink = "https://www.smartera3s.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/smartera-3s-solutions-and-systems/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/smartera3s.com",
                    Description = "An Odoo partner providing comprehensive ERP system implementation, customization, and support for businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 194,
                    Name = "Sodic",
                    Location = "Cairo",
                    CareersLink = "https://careers.sodic.com/",
                    LinkedinLink = "https://www.linkedin.com/company/sodic/",
                    IndustryId = 4,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/sodic.com",
                    Description = "One of Egypt's leading real estate development companies, building large, mixed-use communities and residential projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 195,
                    Name = "sonnen",
                    Location = "New Cairo",
                    CareersLink = "https://sonnen.eg/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/sonnen-egypt/",
                    IndustryId = 6,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/sonnen.de",
                    Description = "A global market leader in smart energy storage systems and innovative energy services for households and small businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 196,
                    Name = "Souhoola",
                    Location = "Cairo",
                    CareersLink = "https://souhoola.com/en/career",
                    LinkedinLink = "https://www.linkedin.com/company/souhoola/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/souhoola.com",
                    Description = "A Buy-Now-Pay-Later (BNPL) fintech company that offers installment payment options for consumers at a network of merchants.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 197,
                    Name = "Speedaf Egypt",
                    Location = "Cairo",
                    CareersLink = "http://www.speedaf.com",
                    LinkedinLink = "https://www.linkedin.com/company/speedafegypt/",
                    IndustryId = 8,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/speedaf.com",
                    Description = "A logistics service provider specializing in express delivery, freight, and warehousing solutions across Africa and the Middle East.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 198,
                    Name = "sprint",
                    Location = "Cairo",
                    CareersLink = "https://career.sprint.xyz/jobs/Careers",
                    LinkedinLink = "https://www.linkedin.com/company/sprint-logistics-eg/",
                    IndustryId = 8,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/sprint.xyz",
                    Description = "A technology-driven company offering on-demand, same-day delivery services for businesses and individuals.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 199,
                    Name = "Sprints",
                    Location = "Cairo",
                    CareersLink = "https://careers.sprints.ai/jobs/Careers/",
                    LinkedinLink = "https://www.linkedin.com/company/sprintsai/",
                    IndustryId = 10,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/sprints.ai",
                    Description = "An EdTech company delivering talent-as-a-service through guaranteed hiring programs in various software fields.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 200,
                    Name = "SSH Design",
                    Location = "Cairo",
                    CareersLink = "https://www.sshic.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/ssh-international/",
                    IndustryId = 9,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/sshic.com",
                    Description = "One of the leading master planning, infrastructure, building design, and construction supervision firms in the Middle East.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 201,
                    Name = "Sumerge",
                    Location = "Cairo",
                    CareersLink = "https://www.sumerge.com/",
                    LinkedinLink = "https://www.linkedin.com/company/sumerge/",
                    IndustryId = 1,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/sumerge.com",
                    Description = "A leading technology solutions provider in the MEA region, offering software development and systems integration services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 202,
                    Name = "SWIFT ACT",
                    Location = "Cairo, Assuit",
                    CareersLink = "http://www.swift-act.com",
                    LinkedinLink = "https://www.linkedin.com/company/swift-act/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/swift-act.com",
                    Description = "A software development company that builds high-end solutions for web, mobile, and IoT platforms.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 203,
                    Name = "Swvl",
                    Location = "Cairo",
                    CareersLink = "https://www.swvl.com/",
                    LinkedinLink = "https://www.linkedin.com/company/swvl/",
                    IndustryId = 8,
                    CompanySize = "1,001-5,000 employees",
                    LogoUrl = "https://logo.clearbit.com/swvl.com",
                    Description = "A global provider of transformative tech-enabled mass transit solutions, offering intercity, intracity, and B2B transportation.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 204,
                    Name = "Sylndr",
                    Location = "Cairo",
                    CareersLink = "https://jobs.lever.co/sylndr",
                    LinkedinLink = "https://www.linkedin.com/company/sylndr/",
                    IndustryId = 7,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/sylndr.com",
                    Description = "An automotive e-commerce marketplace for buying, selling, and financing used cars in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 205,
                    Name = "Sympl",
                    Location = "Cairo",
                    CareersLink = "https://sympl.ai/",
                    LinkedinLink = "https://www.linkedin.com/company/symplfintech/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/sympl.ai",
                    Description = "A fintech company offering a save-your-money and buy-now-pay-later platform, allowing customers to pay in short-term, interest-free installments.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 206,
                    Name = "Synapse Analytics",
                    Location = "Cairo",
                    CareersLink = "https://synapseanalytics.recruitee.com/",
                    LinkedinLink = "https://www.linkedin.com/company/synapse-analytics/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/synapse-analytics.io",
                    Description = "An AI technology company that provides a machine learning operations (MLOps) platform to help businesses build and manage AI models.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 207,
                    Name = "Taager",
                    Location = "Cairo",
                    CareersLink = "https://www.taager.com/sa/",
                    LinkedinLink = "https://www.linkedin.com/company/taagercom/",
                    IndustryId = 7,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/taager.com",
                    Description = "A social commerce platform that provides online sellers with a complete solution including product sourcing, warehousing, and last-mile delivery.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 208,
                    Name = "Tabby",
                    Location = "Cairo",
                    CareersLink = "https://tabby.ai/en-AE/careers",
                    LinkedinLink = "https://www.linkedin.com/company/tabbypay/",
                    IndustryId = 2,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/tabby.ai",
                    Description = "A leading shopping and payments app in the MENA region, offering flexible payment options including Buy-Now-Pay-Later.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 209,
                    Name = "Takka",
                    Location = "Cairo",
                    CareersLink = "http://www.takka.me",
                    LinkedinLink = "https://www.linkedin.com/company/takka-me/",
                    IndustryId = 12,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/takka.me",
                    Description = "A company focused on developing sustainable energy solutions and promoting renewable energy adoption.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 210,
                    Name = "Talaat Moustafa Group",
                    Location = "Cairo",
                    CareersLink = "https://talaatmoustafa.com/",
                    LinkedinLink = "https://www.linkedin.com/company/talaat-moustafa-group/",
                    IndustryId = 4,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/talaatmoustafa.com",
                    Description = "A leading conglomerate in real estate and tourism development in Egypt, known for creating large, integrated urban communities.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 211,
                    Name = "talabat",
                    Location = "Cairo",
                    CareersLink = "https://careers.deliveryhero.com/talabat",
                    LinkedinLink = "https://www.linkedin.com/company/talabat-com/",
                    IndustryId = 7,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/talabat.com",
                    Description = "A leading online food and grocery delivery platform in the MENA region, connecting users with thousands of restaurants and stores.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 212,
                    Name = "Tamara",
                    Location = "Cairo",
                    CareersLink = "https://tamara.co/en-SA/careers",
                    LinkedinLink = "https://www.linkedin.com/company/tamara/",
                    IndustryId = 2,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/tamara.co",
                    Description = "A leading shopping and payments platform in the MENA region, providing Buy-Now-Pay-Later and other financial services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 213,
                    Name = "Tatweer Misr",
                    Location = "Cairo",
                    CareersLink = "https://www.tatweermisr.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/tatweer-misr/",
                    IndustryId = 4,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/tatweermisr.com",
                    Description = "An Egyptian real estate development company renowned for building innovative, sustainable, and high-quality mixed-use communities.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 214,
                    Name = "Telda",
                    Location = "Cairo",
                    CareersLink = "https://jobs.lever.co/telda.app",
                    LinkedinLink = "https://www.linkedin.com/company/telda/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/telda.app",
                    Description = "A fintech company offering a money app and payment card that simplifies sending, spending, and saving money for users in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 215,
                    Name = "Telecom Egypt",
                    Location = "Smart Village",
                    CareersLink = "https://www.te.eg/wps/portal/te/About/Careers/",
                    LinkedinLink = "https://www.linkedin.com/company/telecom-egypt/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/te.eg",
                    Description = "The primary telephone company in Egypt, providing a full range of integrated telecommunications services, including fixed-line, mobile, and data.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 216,
                    Name = "Thndr",
                    Location = "Cairo",
                    CareersLink = "https://thndr-talent.freshteam.com/jobs",
                    LinkedinLink = "https://www.linkedin.com/company/thndrapp/",
                    IndustryId = 2,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/thndr.app",
                    Description = "A digital investment platform that makes it easy to invest in stocks, bonds, and mutual funds directly from a mobile phone.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 217,
                    Name = "Tradeline Stores",
                    Location = "Cairo",
                    CareersLink = "https://tradeline-stores.com/",
                    LinkedinLink = "https://www.linkedin.com/company/tradeline-stores/",
                    IndustryId = 7,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/tradeline-stores.com",
                    Description = "The first and largest Apple Premium Reseller in Egypt, offering the full range of Apple products and accessories.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 218,
                    Name = "Trella",
                    Location = "Cairo",
                    CareersLink = "https://www.trella.app/careers",
                    LinkedinLink = "https://www.linkedin.com/company/trellaapp/",
                    IndustryId = 8,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/trella.app",
                    Description = "A technology platform that operates as a digital freight marketplace, connecting shippers with carriers to move cargo.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 219,
                    Name = "TrianglZ LLC",
                    Location = "Alexandria",
                    CareersLink = "https://trianglz.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/trianglz/about/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/trianglz.com",
                    Description = "A software development company that builds high-quality, scalable mobile and web applications for startups and enterprises.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 220,
                    Name = "TRU",
                    Location = "Cairo",
                    CareersLink = "https://trufinance.app/careers",
                    LinkedinLink = "https://www.linkedin.com/company/truapp/",
                    IndustryId = 2,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/trufinance.app",
                    Description = "A fintech company offering a Buy-Now-Pay-Later platform specifically designed for B2B transactions and business procurement.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 221,
                    Name = "TruKKer",
                    Location = "Suez",
                    CareersLink = "https://trukker.com/careers",
                    LinkedinLink = "https://www.linkedin.com/company/trukkertech/",
                    IndustryId = 8,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/trukker.com",
                    Description = "The largest digital freight network in the MENA region, connecting transporters with cargo owners for logistics services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 222,
                    Name = "Turbo EG",
                    Location = "Cairo",
                    CareersLink = "http://www.turbo.info",
                    LinkedinLink = "https://www.linkedin.com/company/turbologistech/",
                    IndustryId = 7,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/turbo.info",
                    Description = "A quick commerce platform specializing in the ultra-fast delivery of groceries and everyday essentials.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 223,
                    Name = "Unifonic",
                    Location = "Cairo",
                    CareersLink = "http://www.unifonic.com",
                    LinkedinLink = "https://www.linkedin.com/company/unifonic/",
                    IndustryId = 1,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/unifonic.com",
                    Description = "A leading cloud communications platform in the Middle East that provides SMS, voice, and other messaging solutions for businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 224,
                    Name = "Upwyde Developments",
                    Location = "Cairo",
                    CareersLink = "https://upwyde.com/career/",
                    LinkedinLink = "https://www.linkedin.com/company/upwyde-developments/",
                    IndustryId = 4,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/upwyde.com",
                    Description = "A real estate development company in Egypt focused on creating high-quality, innovative commercial and residential projects.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 225,
                    Name = "Valeo",
                    Location = "Smart Village",
                    CareersLink = "https://www.valeo.com/en/find-a-job-or-internship/",
                    LinkedinLink = "https://www.linkedin.com/company/valeo/",
                    IndustryId = 6,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/valeo.com",
                    Description = "A global automotive supplier and technology company that designs innovative solutions for smart mobility, with a large software development center in Egypt.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 226,
                    Name = "Valu",
                    Location = "Cairo",
                    CareersLink = "https://www.valu.com.eg",
                    LinkedinLink = "https://www.linkedin.com/company/valuegypt/",
                    IndustryId = 2,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/valu.com.eg",
                    Description = "A leading Buy-Now-Pay-Later (BNPL) fintech platform in the MENA region, offering accessible and affordable consumer financing solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 227,
                    Name = "Valify Solutions",
                    Location = "Cairo",
                    CareersLink = "https://valify-solutions.zohorecruit.com/jobs/Careers",
                    LinkedinLink = "https://www.linkedin.com/company/valifysolutions/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/valifysolutions.com",
                    Description = "A technology company providing AI-powered Digital Identity solutions for businesses, including user verification and onboarding.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 228,
                    Name = "Vezeeta",
                    Location = "Cairo",
                    CareersLink = "https://careers.vezeeta.com/",
                    LinkedinLink = "https://www.linkedin.com/company/vezeeta/",
                    IndustryId = 3,
                    CompanySize = "501-1,000 employees",
                    LogoUrl = "https://logo.clearbit.com/vezeeta.com",
                    Description = "A leading digital healthcare platform in the MENA region that connects patients with doctors, labs, and pharmacies.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 229,
                    Name = "Vodafone",
                    Location = "Smart Village",
                    CareersLink = "https://careers.vodafone.com/",
                    LinkedinLink = "https://www.linkedin.com/company/vodafone/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/vodafone.com",
                    Description = "A leading global telecommunications company that provides a wide range of services, including mobile, fixed-line, and IoT solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 230,
                    Name = "_VOIS",
                    Location = "Smart Village",
                    CareersLink = "https://jobs.vodafone.com/careers?query=_VOIS",
                    LinkedinLink = "https://www.linkedin.com/company/vois/",
                    IndustryId = 1,
                    CompanySize = "10,001+ employees",
                    LogoUrl = "https://logo.clearbit.com/vodafone.com",
                    Description = "A strategic arm of Vodafone Group, providing high-quality services and building technology solutions for Vodafone's global operations.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 231,
                    Name = "Xceed",
                    Location = "Cairo",
                    CareersLink = "https://www.xceedcc.com/site/careers",
                    LinkedinLink = "https://www.linkedin.com/company/xceed/",
                    IndustryId = 9,
                    CompanySize = "5,001-10,000 employees",
                    LogoUrl = "https://logo.clearbit.com/xceedcc.com",
                    Description = "A leading global provider of multilingual Business Process Outsourcing (BPO) and customer experience management services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 232,
                    Name = "YFS Logistics",
                    Location = "Cairo",
                    CareersLink = "https://www.yfs-logistics.com/",
                    LinkedinLink = "https://www.linkedin.com/company/yalla-fel-sekka/",
                    IndustryId = 8,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/yfs-logistics.com",
                    Description = "A technology-driven logistics company (formerly Yalla Fel Sekka) providing on-demand, last-mile delivery services for businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 233,
                    Name = "Yodawy",
                    Location = "Giza",
                    CareersLink = "https://www.yodawy.com/",
                    LinkedinLink = "https://www.linkedin.com/company/yodawyapp/",
                    IndustryId = 3,
                    CompanySize = "201-500 employees",
                    LogoUrl = "https://logo.clearbit.com/yodawy.com",
                    Description = "A digital pharmacy benefits platform that allows users to order medicines and personal care products from pharmacies online.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 234,
                    Name = "YOUSSEF + PARTNERS Arbitration Leaders",
                    Location = "Cairo",
                    CareersLink = "https://youssef.law/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/youssef-plus-partners/",
                    IndustryId = 9,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/youssef.law",
                    Description = "A leading international law firm based in Egypt, specializing in arbitration, litigation, and corporate law.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 235,
                    Name = "Zaki Hashem, Attorneys at Law",
                    Location = "Cairo",
                    CareersLink = "https://hashemlaw.com/career/",
                    LinkedinLink = "https://www.linkedin.com/company/zaki-hashem-attorneys-at-law/",
                    IndustryId = 9,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/hashemlaw.com",
                    Description = "One of the oldest and largest law firms in Egypt and the Middle East, providing a full range of legal services.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 236,
                    Name = "Zaldi Capital",
                    Location = "New Cairo",
                    CareersLink = "https://zaldi-capital.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/zaldi-capital/",
                    IndustryId = 2,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/zaldi-capital.com",
                    Description = "A venture capital firm focused on investing in and supporting early-stage technology startups in the MENA region.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 237,
                    Name = "Zed Industries",
                    Location = "Cairo",
                    CareersLink = "https://zed.dev/jobs",
                    LinkedinLink = "https://www.linkedin.com/company/zeddotdev/",
                    IndustryId = 1,
                    CompanySize = "11-50 employees",
                    LogoUrl = "https://logo.clearbit.com/zed.dev",
                    Description = "A software company building a high-performance, multiplayer code editor for collaborative software development.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 238,
                    Name = "Zilla Capital",
                    Location = "Cairo",
                    CareersLink = "https://www.zillacapital.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/zillacapitalnew/",
                    IndustryId = 2,
                    CompanySize = "1-10 employees",
                    LogoUrl = "https://logo.clearbit.com/zillacapital.com",
                    Description = "An investment management and financial advisory firm that focuses on creating value in startups and growing businesses.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 239,
                    Name = "ZINAD IT",
                    Location = "Smart Village",
                    CareersLink = "https://zinad.net/jobs.html",
                    LinkedinLink = "https://www.linkedin.com/company/zinad-security-and-software-services/",
                    IndustryId = 1,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/zinad.net",
                    Description = "A technology company specializing in cybersecurity services, software development, and secure IT solutions.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new Company
                {
                    CompanyId = 240,
                    Name = "Zulficar & Partners",
                    Location = "Cairo",
                    CareersLink = "https://zulficarpartners.com/careers/",
                    LinkedinLink = "https://www.linkedin.com/company/zulficar-and-partners-law-firm/",
                    IndustryId = 9,
                    CompanySize = "51-200 employees",
                    LogoUrl = "https://logo.clearbit.com/zulficarpartners.com",
                    Description = "An international corporate law and arbitration firm based in Cairo, providing top-tier legal services to a diverse client base.",
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                }
            ];
        }
    }
}
