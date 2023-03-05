import MyNavLink from "../../atoms/MyNavLink"
import "../../styles/css/index.css"
import Button from "../../atoms/SignButton"
import useInput from "../../hooks/useInput"
import registerValidation from "../../validations/registerValidation"
import { useState } from "react"
import { useNavigate } from "react-router-dom"
import ErrorModal from "../../organisms/ErrorModal"
import { useStateContext } from "../../context/context"

function SignUp(){
	const navigate = useNavigate()
	const [errors, setErrors] = useState([])
	const [modal, setModal] = useState(false)
	const {setPhoto, setHasPhoto} = useStateContext();

	const inputEmail = useInput('')
	const inputPassword = useInput('')
	const inputRepeatPassword = useInput('')
	const inputFirstName = useInput('')
	const inputLastName = useInput('')
	const inputUserame = useInput('')
	const inputCountry = useInput('')
	const inputCity = useInput('')
	const inputDescription = useInput('')
	const inputExp = useInput()
	const inputFile = useInput()

	async function Submit(){
		let error = registerValidation(inputEmail.value, inputPassword.value, inputRepeatPassword.value, inputFirstName.value, inputLastName.value) 
		if(error.length === 0){
			console.log(inputFile.value)
			let userData = {
				"name": inputFirstName.value,
				"lastname": inputLastName.value,
				"email": inputEmail.value,
				"password": inputPassword.value,
				"avatar": inputFile.value,
				"userName": inputUserame.value,
				"city":inputCity.value,
				"country":inputCountry.value,
				"workExperience":Number(inputExp.value),
				"userDescription":inputDescription.value
			}
			let response = await fetch(`http://mirik297-001-site1.ftempurl.com/registration`, {
				method: 'POST',
				headers: {
				'Content-Type': 'application/json',
				},
				mode: 'cors',
				body: JSON.stringify(userData)
				})
			console.log(response)

			if(response.status == 200){
				navigate('/signIn')
			}
			else{
				error = ['Щось пішло не так..']
				setModal(true)
			}
		}
		else{
			setErrors(error)
			setModal(true)
		}
	}

	return(
	<div>
		<img className="bg__page" src={require("../../assets/image.jfif")}></img>
		<div className="container relative">
			<div className="modal__signup">
				<h2 className="title__signup">Реєстрація</h2>
				<form className="signup__flex">
					<input 
						autoFocus
					   type="text"
						value={inputEmail.value} 
						onChange={inputEmail.onChange} 
						className="Input Input__full" 
						placeholder="Електронна пошта">
					</input>
					<div className="flex Input__full">
						<input 
						   type="text"
							value={inputFirstName.value} 
							onChange={inputFirstName.onChange} 
							className="Input Input__half" 
							placeholder="Ім'я">
						</input>
						<input 
						   type="text"
							value={inputLastName.value} 
							onChange={inputLastName.onChange} 
							className="Input Input__half" 
							placeholder="Прізвище">
						</input>
					</div>
					<div className="flex Input__full">
						<input 
						   type="text"
							value={inputCountry.value} 
							onChange={inputCountry.onChange} 
							className="Input Input__half" 
							placeholder="Країна">
						</input>
						<input 
						   type="text"
							value={inputCity.value} 
							onChange={inputCity.onChange} 
							className="Input Input__half" 
							placeholder="Місто">
						</input>
					</div>
					<div className="flex Input__full">
						<input 
						   type="text"
							value={inputUserame.value} 
							onChange={inputUserame.onChange} 
							className="Input Input__half" 
							placeholder="Нікнейм">
						</input>
						<input 
						   type="text"
							value={inputExp.value} 
							onChange={inputExp.onChange} 
							className="Input Input__half" 
							placeholder="Досвід в роках">
						</input>
					</div>
					<input 
					   autoComplete="new-password"
						value={inputDescription.value} 
						onChange={inputDescription.onChange} 
						className="Input Input__full" 
						placeholder="Опис" 
						type={'password'}>
					</input>
					<input 
					   autoComplete="new-password"
						value={inputPassword.value} 
						onChange={inputPassword.onChange} 
						className="Input Input__full" 
						placeholder="Пароль" 
						type={'password'}>
					</input>
					<input 
						autoComplete="new-password"
						value={inputRepeatPassword.value} 
						onChange={inputRepeatPassword.onChange} 
						className="Input Input__full" 
						placeholder="Повторіть пароль" 
						type={'password'}>
					</input>
					<div>
						<label htmlFor="upload-photo">{inputFile.value === undefined ? "Завантажте аву!" : "Готово!"}</label>
						<input 
							type="file"
							onChange={inputFile.onChangeFile} 
							id="upload-photo" >
						</input>
					</div>
				</form>
				<div className="signup__footer">
					<div className="text">
						<p>У вас вже є аккаунт?</p>
						<MyNavLink  to={`/SignIn`}>Увійдіть!</MyNavLink>
					</div>
					<div>
						<Button onClick={Submit}>Готово!</Button>
					</div>
				</div>
			</div>
			<div className="bg-sign">
				<svg viewBox="0 0 190 483" fill="none" xmlns="http://www.w3.org/2000/svg">
<g clip-path="url(#clip0_8_70)">
<path fillRule="evenodd" clipRule="evenodd" d="M28.0395 0.356576C43.159 36.3059 68.8609 61.2837 82.4736 98.6637C90.5458 120.831 93.9107 144.933 94.1147 169.619C94.2256 183.027 93.9924 196.443 93.9296 209.851C93.8687 222.927 93.5135 235.919 92.4043 248.91C91.2897 261.971 90.0418 274.876 89.4913 288.017C89.0628 298.257 88.8671 310.166 85.3984 319.506C82.6401 326.934 75.6294 328.022 73.7692 319.014C72.7355 314.006 72.8658 308.415 72.9798 303.268C73.126 296.665 73.5099 290.072 73.7056 283.47C74.4924 256.977 73.4724 230.479 74.0056 203.976C74.5264 178.082 77.8528 152.215 74.1721 126.494C71.1328 105.253 63.4114 87.0128 54.5378 69.5266C46.0004 52.7032 36.7085 36.2887 30.9392 17.1523C29.2826 11.6564 28.0019 5.9613 27.3875 0.0516235C27.656 -0.0731856 27.8734 0.0282602 28.0395 0.356576Z" fill="#C1DEE2"/>
<path fillRule="evenodd" clipRule="evenodd" d="M23.9187 71.2801C26.9126 73.4434 28.5856 76.6079 30.6092 79.7302C33.1256 83.612 36.0842 87.1165 38.6 91.0012C43.846 99.1 48.4549 107.696 53.2369 116.123C62.3158 132.118 69.209 149.166 72.623 167.671C76.162 186.848 77.1162 206.642 78.6006 226.106C80.1751 246.735 81.7605 267.514 81.969 288.222C82.0623 297.5 82.5635 307.349 81.4504 316.561C80.7574 322.296 78.1708 327.184 72.3393 327.138C59.9525 327.04 54.1371 313.312 51.7075 302.053C48.1312 285.474 47.4177 267.864 47.9839 250.91C48.6679 230.467 52.2139 210.33 53.7485 189.966C55.3738 168.407 54.4556 147.23 47.0483 126.931C43.3061 116.673 38.9629 106.661 35.063 96.4716C33.4023 92.1331 32.0421 87.6765 29.7991 83.6366C27.3038 79.1428 24.2063 75.0636 22.2522 70.2234C22.8075 70.5761 23.3634 70.9281 23.9187 71.2801Z" fill="#89C5CC"/>
<path fillRule="evenodd" clipRule="evenodd" d="M166.608 28.2909C166.988 27.9956 167.368 27.7003 167.748 27.4043C166.654 32.0162 164.678 35.7624 163.168 39.9782C161.81 43.7683 161.122 48.0572 160.207 52.1999C158.06 61.9298 155.578 71.4404 153.55 81.2532C149.535 100.671 150.294 121.59 152.928 143.133C155.415 163.483 159.348 183.802 161.222 204.147C162.775 221.021 163.443 238.42 161.961 254.519C160.954 265.452 157.653 278.506 148.659 277.403C144.425 276.883 142.216 271.784 141.326 266.027C139.897 256.781 139.598 247.059 139.04 237.865C137.797 217.343 137.549 196.884 137.304 176.573C137.071 157.41 136.431 137.867 137.711 119.187C138.945 101.161 142.806 84.9185 148.325 69.9318C151.232 62.0361 154.002 53.9557 157.269 46.4305C158.835 42.8209 160.749 39.6313 162.316 36.0246C163.576 33.1235 164.578 30.1466 166.608 28.2909Z" fill="#C1DEE2"/>
<path fillRule="evenodd" clipRule="evenodd" d="M27.7449 130.687C29.3828 127.936 32.9883 137.01 33.537 138.151C33.3049 137.584 33.0719 137.017 32.8369 136.45C36.0881 141.86 39.0624 147.491 40.9117 153.694C42.7416 159.834 44.3134 166.098 46.0304 172.282C49.2522 183.884 53.4083 195.157 56.3273 206.864C59.1715 218.272 59.8887 229.486 59.2001 241.286C58.4845 253.55 58.0467 266.002 58.8816 278.259C59.2803 284.114 61.1813 289.567 62.4843 295.203C63.7888 300.844 64.8162 306.561 66.3585 312.132C66.7125 311.407 66.7576 313.833 66.7568 314.017C66.7471 315.409 66.5529 316.576 66.1054 317.896C64.9553 321.29 62.8038 324.173 59.5299 325.11C53.6135 326.801 48.1201 321.416 46.2122 315.73C43.4751 307.57 41.9669 298.554 40.6625 289.978C39.4666 282.114 39.1836 274.204 39.5389 266.232C40.2388 250.51 43.0821 234.995 43.8512 219.278C44.6883 202.168 41.3003 186.091 38.4636 169.435C37.0973 161.41 35.7915 153.46 33.1325 145.814C32.0559 142.718 30.6523 139.986 29.1806 137.126C28.3432 135.5 26.6498 132.598 27.7449 130.687Z" fill="#C1DEE2"/>
<path fillRule="evenodd" clipRule="evenodd" d="M75.7469 103.96C75.0683 123.562 73.8981 143.016 71.2863 162.411C68.6207 182.204 65.5592 201.93 62.9944 221.741C60.4977 241.025 57.8808 260.629 58.2297 280.182C58.4866 294.584 56.0999 318.949 72.086 326.057C88.2766 333.256 91.7495 302.514 92.8127 292.368C94.85 272.9 94.1904 253.227 93.2017 233.653C92.1866 213.564 90.4921 193.577 88.3921 173.561C86.0595 151.328 84.2293 128.994 82.7604 106.698C82.0336 95.6698 81.8546 84.6597 82.0551 73.6286C82.2447 63.2193 81.8494 52.7023 78.1508 42.8191C77.957 42.9597 77.7645 43.1003 77.5707 43.2403" fill="#69A1AC"/>
<path fillRule="evenodd" clipRule="evenodd" d="M159.331 166.062C156.419 170.306 153.484 174.509 150.734 179.005C145.814 187.049 141.365 195.995 140.027 207.496C137.226 231.567 139.473 257.564 140.015 282.371C140.126 287.441 139.485 292.357 141.482 297.181C143.293 301.556 146.678 304.89 149.904 304.872C156.811 304.833 155.757 287.908 155.769 280.458C155.79 267.402 154.326 254.212 154.452 241.168C154.578 228.244 155.364 215.285 157.236 203.011C159.132 190.587 161.908 178.658 165.572 167.525C166.081 165.98 169.73 154.042 166.282 156.099" fill="#89C5CC"/>
<path fillRule="evenodd" clipRule="evenodd" d="M128.827 37.6809C124.286 37.6809 124.314 60.3339 124.177 63.2436C123.684 73.7439 123.8 84.2469 123.488 94.7493C122.804 117.669 121.965 140.789 118.525 163.443C116.672 175.639 114.525 187.78 112.215 199.883C109.898 212.021 106.788 224.144 105.264 236.436C103.801 248.253 104.628 260.302 104.829 272.176C105.029 284.123 105.39 296.093 106.954 307.934C107.34 310.857 107.793 313.77 108.277 316.675C108.68 319.096 108.445 322.099 109.128 324.399C110.222 328.084 114.912 329.29 118.07 328.714C126.616 327.153 132.075 316.416 134.994 308.571C138.722 298.554 140.7 287.689 142.652 277.132C144.864 265.173 146.16 253.031 146.768 240.855C148.004 216.109 146.418 191.336 144.646 166.669C143.661 152.951 142.523 139.293 140.781 125.66C139.046 112.085 136.821 98.5706 135.372 84.9556C134.319 75.0614 133.282 65.1539 132.6 55.2218C132.437 52.8536 132.3 37.6809 128.827 37.6809Z" fill="#69A1AC"/>
<path fillRule="evenodd" clipRule="evenodd" d="M123.142 202.344C122.402 178.56 112.847 74.0924 110.673 45.332C110.13 38.1333 104.896 8.16071 104.217 0C102.377 2.30665 102.534 38.2109 102.554 46.4608C102.575 54.6215 101.564 62.6469 101.044 70.7637C100.513 79.0459 88.2767 186.008 86.6888 193.966C83.5296 209.798 81.4153 226.045 79.7846 242.232C78.196 258.01 75.7261 274.656 77.8519 290.516C78.3406 294.168 79.1138 297.76 80.3727 301.115C81.7328 304.739 82.665 308.09 83.7568 311.883C85.5137 317.991 88.6919 323.352 93.5756 325.874C103.588 331.045 108.964 319.34 111.69 309.149C116.043 292.874 118.836 276.334 120.146 259.242C121.595 240.332 123.734 221.384 123.142 202.344Z" fill="#89C5CC"/>
<path d="M169.46 272.33H20.5406C18.6499 272.33 17.1172 273.863 17.1172 275.755V424.766C17.1172 426.658 18.6499 428.192 20.5406 428.192H169.46C171.35 428.192 172.883 426.658 172.883 424.766V275.755C172.883 273.863 171.35 272.33 169.46 272.33Z" fill="#DA3C39"/>
<path d="M13.6937 289.458H3.42342C1.53272 289.458 0 290.991 0 292.883V479.575C0 481.466 1.53272 483 3.42342 483H13.6937C15.5844 483 17.1171 481.466 17.1171 479.575V292.883C17.1171 290.991 15.5844 289.458 13.6937 289.458Z" fill="#A92D2A"/>
<path d="M99.2794 289.458H89.0091C87.1184 289.458 85.5857 290.991 85.5857 292.883V479.575C85.5857 481.466 87.1184 483 89.0091 483H99.2794C101.17 483 102.703 481.466 102.703 479.575V292.883C102.703 290.991 101.17 289.458 99.2794 289.458Z" fill="#A92D2A"/>
<path d="M186.577 289.458H176.306C174.416 289.458 172.883 290.991 172.883 292.883V479.575C172.883 481.466 174.416 483 176.306 483H186.577C188.467 483 190 481.466 190 479.575V292.883C190 290.991 188.467 289.458 186.577 289.458Z" fill="#A92D2A"/>
</g>
<defs>
<clipPath id="clip0_8_70">
<rect width="190" height="483" fill="white"/>
</clipPath>
</defs>
				</svg>
			</div>
		</div>
		<ErrorModal error = {errors} setModal={setModal} modal={modal}></ErrorModal>
	</div>
	)
}

export default SignUp