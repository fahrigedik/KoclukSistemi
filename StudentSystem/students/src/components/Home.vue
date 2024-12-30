<template>
    <div class="homeContainer">
      <div class="container text-center py-5">
        <div class="info-box mb-5">
          <h1 class="fw-bold display-5 mb-4">Koçluk Nedir?</h1>
          <p class="fs-5">
            Öğrencilerin ihtiyaç duydukları alanlarda koçluk yapılması gerekmektedir. Onları sınavlara etkili bir şekilde yönlendirir ve hazırlar. 
            Öğrenci koçluğunda en önemli unsur öğrenci ve koç arasındaki ilişkidir: uyum ve işbirliği. Genel amaç, öğrencilerin belirledikleri hedeflerde başarıya ulaşmaları için onları doğru yöne yönlendirmektir.
          </p>
        </div>
  
     
        <div class="row gx-5">
          <div class="col-md-6">
            <div class="card coach-card">
              <h2 class="mb-3">Koç İçin</h2>
              <p>
                Öğrencinin hedeflerine ulaşmasında destek olur, öğrenme motivasyonunu artırır, akademik çabalarında sonuç almasını sağlar ve ihtiyaç duyduğu becerileri geliştirir.
              </p>
              <button class="btn btn-lg btn-outline-light mt-3">Giriş</button>
            </div>
          </div>
  
          <div class="col-md-6">
            <div class="card student-card">
              <h2 class="mb-3">Öğrenci İçin</h2>
              <p>
                Ders çalışma alışkanlıklarını geliştirir, sınavlara hazırlıklarını planlar ve akademik hedef belirlemelerine yardımcı olur. Bu sayede öğrenciler daha yüksek notlar alabilir ve derslerinde daha başarılı olabilirler.
              </p>
              <button class="btn btn-lg btn-outline-secondary mt-3" @click="$router.push('/login')">Giriş</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  </script>
  
  <style scoped>
  /* Sayfa arka planı */
  .homeContainer {
    min-height: 110vh;
    display: flex;
    align-items: center;
    
  }
    
  /* Kartların ortak stili */
  .card {
    padding: 30px ;
    border-radius: 15px;
    text-align: center;
    box-shadow: 0 4px 5px rgba(0, 0, 0, 0.2);
  }
  
  /* Koç kartı */
  .coach-card {
    background: linear-gradient(135deg, #1d1e22, #3b3d42); /* Siyah ve gri degrade */
    color: white;
  }
  
  /* Öğrenci kartı */
  .student-card {
    background: #f8f9fa; /* Açık gri arka plan */
    border: 1px solid #dee2e6;
  }
  
  /* Responsive uyum için boşluklar */
  /* @media (max-width: 768px) {
    .info-box {
      padding: 20px;
    }
    .card {
      margin-bottom: 20px;
    }
  } */
  </style>
  