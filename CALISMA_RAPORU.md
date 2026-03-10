PROJE ÇALIŞMA RAPORU
1. Proje Özeti
Bu çalışma kapsamında, kurumsal düzeyde bir Akıllı Depo Yönetim Sistemi geliştirilmiştir. Uygulama; ürünlerin tanımlanması, stok miktarlarının anlık takibi, kritik stok uyarıları ve gelişmiş filtreleme özelliklerini içermektedir. Temel hedef, büyük veri setlerinde bile performanslı çalışan, veri güvenliğini (Soft Delete) ön planda tutan ve kullanıcı dostu bir arayüz sunan bir "Full-Stack" çözüm üretmektir.

2. Kullanılan Teknolojiler ve Versiyonlar
Sistemin güncel ve sürdürülebilir olması için en son kararlı sürümler tercih edilmiştir:

Backend:

Framework: .NET 9.0 (Cross-platform yüksek performans)

ORM: Entity Framework Core 9.0

Veritabanı: Microsoft SQL Server

Mimari: Layered Architecture (Katmanlı Mimari)

Frontend:

Kütüphane: React 18

Dil: TypeScript (Tip güvenliği için)

UI Framework: Material UI (MUI) v6

HTTP Client: Axios

3. Karşılaşılan Sorunlar ve Çözüm Yolları
Geliştirme sürecinde karşılaşılan temel teknik zorluklar ve aşılma yöntemleri şunlardır:

CORS Hataları: Frontend ve Backend farklı portlarda çalıştığı için tarayıcı engeline takılınmıştır.

Çözüm: Program.cs üzerinde AllowAll politikasıyla CORS middleware'i doğru sırayla (Routing öncesi) eklenerek sorun çözülmüştür.

Interface (Arabirim) Uyuşmazlığı: IProductManager içindeki metod imzalarıyla ProductManager implementasyonu arasında oluşan Task dönüş tipi farklılıkları derleme hatalarına yol açmıştır.

Çözüm: Metot imzaları asenkron yapıya (async/await) tam uyumlu hale getirilmiş ve eksik metotlar tamamlanmıştır.

Veri Tutarlılığı: Silinen verilerin geçmişe yönelik raporlamada kaybolması riski fark edilmiştir.

Çözüm: Fiziksel silme yerine Soft Delete mekanizmasına geçilmiştir.

4. Mimari Kararlar ve Nedenleri
Repository Pattern: Veri erişim mantığını iş mantığından ayırmak ve kodun test edilebilirliğini artırmak için tercih edilmiştir.

Soft Delete Mekanizması: Ticari sistemlerde veri kaybının önlenmesi kritik olduğu için veriler veritabanından silinmek yerine IsDeleted bayrağı ile işaretlenmiş, tüm listeleme sorguları bu kritere göre filtrelenmiştir.

POST Metot Önceliği: Güvenlik ve firewall kısıtlamaları düşünülerek, PUT ve DELETE yerine tüm güncelleme/silme işlemleri POST metotları üzerinden, güçlü tipteki DTO'lar ile yapılmıştır.

Server-Side Pagination: UI tarafında kasılmaları önlemek adına 1 milyon veri olsa dahi sadece aktif sayfadaki 25 veriyi çekecek şekilde sunucu taraflı sayfalama uygulanmıştır.

5. Yapay Zeka (AI) Kullanım Detayları
Proje geliştirme sürecinde yapay zeka bir "Pair Programmer" (Eş Yazılımcı) olarak şu aşamalarda kullanılmıştır:

Mimarinin İskeletlendirilmesi: Klasör yapısının ve DTO (Data Transfer Object) sınıflarının standartlara uygun şablonlarının hızlıca oluşturulmasında AI desteği alınmıştır.

Hata Ayıklama (Debugging): Özellikle C# derleme hatalarının (Interface implementation errors) ve React tarafındaki useEffect bağımlılık döngülerinin hızlı analiz edilmesinde kullanılmıştır.

UI Komponent Seçimi: Material UI kütüphanesindeki karmaşık tablo ve modal yapılarının özelleştirilmesi sırasında "best-practice" örneklerine ulaşmak için AI araçlarından yararlanılmıştır.

Kod Optimizasyonu: Yazılan metodların daha temiz (clean code) ve okunabilir hale getirilmesi için refactoring önerileri alınmıştır.

📝 Geliştirici Notu
Sistem, genişletilebilir bir yapıda kurulmuştur. Gelecek aşamalarda "Stok Hareket Geçmişi" (Inventory Transactions) ve "Kullanıcı Yetkilendirme" (Identity Server) modülleri mevcut mimariye kolayca entegre edilebilir