import axios from 'axios';

// Backend adresimiz (Siyah ekranda yazan https adresi)
const api = axios.create({
  baseURL: 'https://localhost:7104/api', 
});

export default api;