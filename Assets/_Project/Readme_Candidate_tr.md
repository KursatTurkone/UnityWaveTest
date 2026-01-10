# Unity C# Geliştirici Teknik Görev

Bu görevi tamamlamak için ayırdığınız zaman için teşekkürler.

Bu proje **küçük bir Unity 2D yukarıdan bakış (top-down) prototipidir**. Kod tabanının bazı kısımları bilerek kaba veya prototip seviyesinde bırakılmışken, bazı kısımlar ise temiz ve iyi yapılandırılmıştır. Bu durum, yeni sistemlerin mevcut kodun yanında geliştirilmesi gereken gerçek dünya koşullarını yansıtır.

Hedefiniz, **temel bir oyun sistemini uygulamak** ve projeye entegre etmektir.

---

## Projeye Genel Bakış

- Oyun Motoru: **Unity (2D)**
- Input Sistemi: **Eski Input Manager**
- Görseller: **Yalnızca basit şekiller**
- Sahne: `Main.unity`
- Oyuncu (Player) kodu bilerek **temiz ve iyi yapılandırılmıştır**
- Diğer sistemler **tek parça (monolithic), birbirine bağımlı (coupled) veya tutarsız** olabilir

> **Tüm projeyi baştan yazmanız beklenmiyor.**

---

## Göreviniz

### Dalga (Wave) Sistemi
Dalgalar halinde düşman ilerleme (wave-based enemy progression) sistemi uygulayın.

**Gereksinimler**
- Oyun **Dalga 1** ile başlar
- Dalga başına düşman sayısı:
    - Dalga 1: **5 düşman**
    - Her sonraki dalga: **+3 düşman**
- Düşmanlar **zamana yayılarak** doğmalı, hepsi aynı anda değil:
    - Doğma aralığı (spawn interval): **0.5 saniye**
- Bir dalga, o dalgaya ait **tüm düşmanlar yenildiğinde** tamamlanır
- Dalgalar arasında:
    - **3 saniyelik ara**
    - Sonraki dalga otomatik olarak başlar
- UI’da mevcut dalgayı gösterin:
    - `Wave: X`

> Mevcut doğma (spawning) mantığını gerektiği şekilde yeniden düzenleyebilir (refactor) veya değiştirebilirsiniz.

---

## Kısıtlar ve Beklentiler

- **Oyuncu scriptleri bilerek iyi yapılandırılmıştır**
    - Yeniden yazmanız gerekmemeli
- Diğer sistemlerin düzeltilmesi veya refactor edilmesi gerekebilir
- **Hedefli ve bilinçli iyileştirmeler** yapın
- Açıkça gerekçelendirilmedikçe büyük çaplı yeniden yazımlardan kaçının
- Kod okunabilir ve sürdürülebilir olmalıdır

---

## Teslim Edilecekler

Lütfen çözümünüzü **GitHub Pull Request** olarak gönderin.

İçermeli:
1. Unity projesinde çalışan uygulama
2. PR açıklamasında veya README yorumunda kısa bir açıklama:
    - Ne uyguladınız
    - Neyi refactor ettiniz ve neden
    - Herhangi bir ödün (trade-off) veya bilinen sorunlar
> Düşünce sürecinizi gösteren commit’ler daha iyi olur. Tek büyük commit yerine, ilerleyişinizi gösteren daha küçük commit’ler yapmaya çalışın.

---

## Zaman Beklentisi

- Hedef süre: **1 gün**
- Bu, hız veya cilalama testi değildir
- Doğruluk, netlik ve muhakemeye odaklanın

---

## Değerlendirme Kriterleri

Şunlara bakacağız:
- Mevcut kodu anlama ve onunla çalışma becerisi
- Temiz ve doğru C# kullanımı
- Unity temelleri (MonoBehaviour’lar, yaşam döngüsü, prefab’lar)
- Sorumlulukların ayrımı
- Mantıklı refactor kararları
- Dalga, skor ve yeniden başlatma (restart) mantığının sağlamlığı

---

## Opsiyonel (Bonus)
Zaman kalırsa isteğe bağlı olarak ekleyebilirsiniz:
- Object pooling
- Dalgalar için ScriptableObject ile konfigürasyon
- Basit otomatik testler
- Küçük mimari iyileştirmeler

Bunlar **zorunlu değildir**.

---

## Notlar
Bu proje bilerek kusurlar içerir.  
Bizim için, mükemmel veya aşırı mühendislik yapılmış bir çözüm üretmekten çok, **nasıl düşündüğünüz, entegre ettiğiniz ve iyileştirdiğiniz** daha önemlidir.

> İyi şanslar ve zamanınız için teşekkürler.
