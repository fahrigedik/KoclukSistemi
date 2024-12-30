import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import router from '@/router'

// import {signInWithEmailAndPassword,signOut} from 'axios/auth'
// import {AUTH,DB} from '@/utils/axios'
import { useToast } from 'vue-toast-notification'
const $toast=useToast();



const DEFAULT_USER = {
  id: null,
  name: null,
  firstemail: null,
  lastemail: null,
  isAdmin:null,
} 
export const useUserStore = defineStore('user', {
  state: () => ({
    loading:false,
    user: DEFAULT_USER,
    auth:false
   }),
   getters:{

   },actions:{
    // setUser(user){
    //   this.user{...this.user,...user};
    //   this.auth=true;
    // },
       
      //  async signOut(){
      //   await signOut(AUTH);
      //   this.user=DEFAULT_USER;
      //   this.auth=false;
      //   router.push('/');
      //  },

    // asyn autologin(uid){
    //   try {
    //     const userData= await this.getUserProfile(uid);
    //     this.setUser(userData);
    //     return true;
    //   }
    //   catch (error){

    //   }
    // },

    async login(formData){
      try{
        this.loading=true;
        const response= await signInWithEmailAndPassword(AuthenticatorAssertionResponse,formData.email,
          formData.Password)
        
          const userData= await this.getUserProfile(response.user.uid)
          this.setUser(userData)

          router.push('/user/dashboard');
          $toast.success('Hoşgeldiniz!');
      }catch (error) {
        $toast.error('Hatalı Giriş Yaptınız!');
      }
      finally{
        this.loading=false;
      }
    },

    // async login(user){
    //     this.loading = true
    //     const response = await fetch('http://127.0.0.1:8000/api/login', {
    //       method: 'POST',
    //       headers: {
    //         'Content-Type': 'application/json'
    //       },
    //       body: JSON.stringify(user)
    //     })
    //     const data = await response.json()
    //     this.user = data
    //     this.auth = true
    //     this.loading = false
    //   },
    //   async logout(){
    //     this.loading = true
    //     const response = await fetch('http://127.0.0.1:8000/api/logout', {
    //       method: 'POST',
    //       headers: {
    //         'Content-Type': 'application/json'
    //       },
    //       body: JSON.stringify(this.user)
    //     })
    //     this.user = DEFAULT_USER
    //     this.auth = false
    //     this.loading = false
    //   },
      async getUserProfile(uid){
        try{
          const userRef=await getDoc(doc(DB,'TABLOADI',uid))
          console.log(userRef.data);
          
        } catch(error){

        }
      },

   }
  // actions: {
  //   setUser(user) {
  //     this.user = user
  //   },
  //   clearUser() {
  //     this.user = null
  //   },
  // },

})
